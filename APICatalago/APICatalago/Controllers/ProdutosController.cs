using APICatalago.Context;
using APICatalago.Filters;
using APICatalago.Interfaces;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Os try/catch foram removimos porque esta sendo trato as exceções não esperadas no middleware ApiExceptionFilter

namespace APICatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;

        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> ConsultaProdutos()
        {
            //O AsNoTracking melhora o desempenho da consulta, eliminando o cache das entidades do contexto
            //Deve ser usado paneas em funções de leitura aonde eu não sei o estado atual dos objetos.
            var produtos = _repository.GetProdutos();
            if (!produtos.Any())
            {
                return NotFound("Produtos não encontrados...");
            }
            return Ok(produtos);
        }

        // Para evitar acesso de rotas desnecessarias foi colocado o parâmetro "min(1)", essa parâmetro é uma restrição de rota,
        // ele limita um valor mínimo para a chamada da rota, nesse caso, se for passado um id = 0, não é realizado o acesso a rota.
        // A restrição de rota é utilizada para distinguir rotas parecidas
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<Produto> ConsultaProduto(int id)
        {
            var produto = _repository.GetProduto(id);
            if (produto is null)
            {
                return NotFound($"Produto com id = {id} não encontrado...");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult CadastraProduto(Produto produto)
        {
            if (produto is null) return BadRequest("Dados enviados são inválidos!");

            var novoProduto = _repository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult AlterarProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Dados enviados são inválidos!");
            }

            var alterouProduto = _repository.Update(produto);

            if (alterouProduto)
            {
                return Ok(alterouProduto);
            }
            else
            {
                return StatusCode(500, $"Falha ao atualizar o produto de id = {id}");
            }

        }

        [HttpDelete("{id:int}")]
        public ActionResult ExcluirProduto(int id)
        {
            var deletado = _repository.Delete(id);

            if (deletado)
            {
                return Ok($"Produto de id={id} foi excluído");
            }
            else
            {
                return StatusCode(500, $"Falha ao excluir o produto com id= {id}");
            }

        }
    }
}
