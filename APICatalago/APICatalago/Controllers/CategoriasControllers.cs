
using APICatalago.Interfaces;
using APICatalago.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasControllers : ControllerBase
    {
        private readonly IUnitOfWork _ufw;
        private readonly ILogger _logger;

        public CategoriasControllers(IUnitOfWork ufw, ILogger<CategoriasControllers> logger)
        {
            _ufw = ufw;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> ConsultaCategorias() 
        {
            //Exemplos de utilização de logger
            _logger.LogInformation("======================= GET catalago/ =========================");

            var categorias = _ufw.CategoriaRepository.GetAll();
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
            var categoria = _ufw.CategoriaRepository.Get(c => c.CategoriaId == id);
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

            var categoriaCriada = _ufw.CategoriaRepository.Create(categoria);
            _ufw.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult AlteraCategoria(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Dados enviados são inválidos!");
            }

            var categoriaAlterada = _ufw.CategoriaRepository.Update(categoria);
            _ufw.Commit();

            return Ok(categoriaAlterada);
        }

        [HttpDelete("{id:int}")]
        public ActionResult ExcluiCategoria(int id) 
        {
            var categoria = _ufw.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound($"Categoria com id = {id} não encontrada...");
            }

            var categoriaExcluida = _ufw.CategoriaRepository.Delete(categoria);
            _ufw.Commit();

            return Ok(categoriaExcluida);
        }
    }
}
