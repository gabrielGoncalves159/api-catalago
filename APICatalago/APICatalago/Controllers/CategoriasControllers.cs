
using APICatalago.Context;
using APICatalago.Interfaces;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasControllers : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly ILogger _logger;

        public CategoriasControllers(ICategoriaRepository repository, ILogger<CategoriasControllers> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> ConsultaCategoriasProdutos()
        {
            var categorias = _repository.GetCategoriasProdutos();

            if (!categorias.Any())
            {
                return NotFound("Categorias dos produtos não encontradas...");
            }


            return Ok(categorias);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> ConsultaCategorias() 
        {
            //Exemplos de utilização de logger
            _logger.LogInformation("======================= GET catalago/ =========================");

            var categorias = _repository.GetCategorias();
            if (!categorias.Any())
            {
                _logger.LogInformation("======== Nenhum catalago encontrado ============");
                return NotFound("Categorias não econtradas...");
            }

            _logger.LogInformation("======== Retornou catalago encontrado ============");
            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name= "ObterCategoria")]
        public ActionResult<Categoria> ConsultaCategoria(int id)
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria == null)
            {
                return NotFound($"Categoria com id = {id} não encontrada...");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult CadastraCategoria(Categoria categoria)
        {
            if (categoria == null) return BadRequest("Dados enviados são inválidos!");

            var categoriaCriada = _repository.Create(categoria);

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult AlteraCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Dados enviados são inválidos!");
            }

            var categoriaAlterada = _repository.Update(categoria);

            return Ok(categoriaAlterada);
        }

        [HttpDelete("{id:int}")]
        public ActionResult ExcluiCategoria(int id) 
        {
            var categoria = _repository.GetCategoria(id);
            if (categoria == null)
            {
                return NotFound($"Categoria com id = {id} não encontrada...");
            }

            var categoriaExcluida = _repository.Delete(id);

            return Ok(categoriaExcluida);
        }
    }
}
