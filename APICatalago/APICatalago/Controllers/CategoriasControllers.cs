
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
            return _context.Categorias.Include(p => p.Produtos).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> ConsultaCategorias() 
        { 
            var categorias = _context.Categorias.ToList();
            if (!categorias.Any())
            {
                return NotFound("Categorias não econtradas...");
            }

            return categorias;
        }

        [HttpGet("{id:int}", Name= "ObterCategoria")]
        public ActionResult<Categoria> ConsultaCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada...");
            }

            return categoria;
        }

        [HttpPost]
        public ActionResult CadastraCategoria(Categoria categoria)
        {
            if (categoria == null) return BadRequest();

            _context.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria );
        }

        [HttpPut("{id:int}")]
        public ActionResult AlteraCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            { 
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult ExcluiCategoria(int id) 
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada...");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
