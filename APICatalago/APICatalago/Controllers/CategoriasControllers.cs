
using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasControllers : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;

        public CategoriasControllers(AppDbContext context, ILogger<CategoriasControllers> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> ConsultaCategoriasProdutos()
        {
            try
            {
                //TODO: Nesse trecho de código o ideal é realizar uma filtragem, nunca retornar todos os registros de uma vez, isso resulta
                // em problema de desenpanho, nesse capo pode-se utilizar o método 'Where()' para realizar um filtro
                return _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
            }
            catch (Exception) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> ConsultaCategorias() 
        {
            try
            {
                //Exemplos de utilização de logger
                _logger.LogInformation("======================= GET catalago/ =========================");

                //O AsNoTracking melhora o desempenho da consulta, eliminando o cache das entidades do contexto
                //Deve ser usado paneas em funções de leitura aonde eu não sei o estado atual dos objetos.
                var categorias = _context.Categorias.AsNoTracking().ToList();
                if (!categorias.Any())
                {
                    _logger.LogInformation("======== Nenhum catalago encontrado ============");
                    return NotFound("Categorias não econtradas...");
                }

                _logger.LogInformation("======== Retornou catalago encontrado ============");
                return categorias;
            }
            catch (Exception)
            {
                _logger.LogInformation($"======== Um erro ocorrou na requisição de catalagos ============");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name= "ObterCategoria")]
        public ActionResult<Categoria> ConsultaCategoria(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"Categoria com id = {id} não encontrada...");
                }

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult CadastraCategoria(Categoria categoria)
        {
            try
            {
                if (categoria == null) return BadRequest("Dados enviados são inválidos!");

                _context.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult AlteraCategoria(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest("Dados enviados são inválidos!");
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult ExcluiCategoria(int id) 
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
                if (categoria == null)
                {
                    return NotFound($"Categoria com id = {id} não encontrada...");
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
