using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> ConsultaProdutos()
        {
            try
            {
                //O AsNoTracking melhora o desempenho da consulta, eliminando o cache das entidades do contexto
                //Deve ser usado paneas em funções de leitura aonde eu não sei o estado atual dos objetos.
                var produtos = _context.Produtos.AsNoTracking().ToList();
                if (!produtos.Any())
                {
                    return NotFound("Produtos não encontrados...");
                }
                return produtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> ConsultaProduto(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com id = {id} não encontrado...");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult CadastraProduto(Produto produto)
        {
            try
            {
                if (produto is null) return BadRequest("Dados enviados são inválidos!");

                _context.Add(produto);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult AlterarProduto(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return BadRequest("Dados enviados são inválidos!");
                }

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(produto);
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult ExcluirProduto(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(x => x.ProdutoId == id);
                if (produto is null)
                {
                    return NotFound($"Produto com id = {id} não encontrado para exclusão...");
                }

                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                return Ok(produto);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
