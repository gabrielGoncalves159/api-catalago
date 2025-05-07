
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

        public CategoriasControllers(AppDbContext context)
        {
            _context = context;
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
                //O AsNoTracking melhora o desempenho da consulta, eliminando o cache das entidades do contexto
                //Deve ser usado paneas em funções de leitura aonde eu não sei o estado atual dos objetos.
                var categorias = _context.Categorias.AsNoTracking().ToList();
                if (!categorias.Any())
                {
                    return NotFound("Categorias não econtradas...");
                }

                return categorias;
            }
            catch (Exception)
            {
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
