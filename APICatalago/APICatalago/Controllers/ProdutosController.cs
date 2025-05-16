using APICatalago.Context;
using APICatalago.Filters;
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
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ConsultaProdutos()
        {
            //O AsNoTracking melhora o desempenho da consulta, eliminando o cache das entidades do contexto
            //Deve ser usado paneas em funções de leitura aonde eu não sei o estado atual dos objetos.
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            if (!produtos.Any())
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }

        // Para evitar acesso de rotas desnecessarias foi colocado o parâmetro "min(1)", essa parâmetro é uma restrição de rota,
        // ele limita um valor mínimo para a chamada da rota, nesse caso, se for passado um id = 0, não é realizado o acesso a rota.
        // A restrição de rota é utilizada para distinguir rotas parecidas
        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<Produto>> ConsultaProduto(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);
            if (produto is null)
            {
                return NotFound($"Produto com id = {id} não encontrado...");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult CadastraProduto(Produto produto)
        {
            if (produto is null) return BadRequest("Dados enviados são inválidos!");

            _context.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult AlterarProduto(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest("Dados enviados são inválidos!");
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult ExcluirProduto(int id)
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
    }
}
