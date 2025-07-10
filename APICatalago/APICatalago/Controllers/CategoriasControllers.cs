
using APICatalago.Dtos;
using APICatalago.Dtos.Mappings;
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
        public ActionResult<IEnumerable<CategoriaDTO>> ConsultaCategorias() 
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

            var categoriasDTO = categorias.ToCategoriaDTOList();
            return Ok(categoriasDTO);
        }

        [HttpGet("{id:int}", Name= "ObterCategoria")]
        public ActionResult<CategoriaDTO> ConsultaCategoria(int id)
        {
            var categoria = _ufw.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound($"Categoria com id = {id} não encontrada...");
            }

            var categoriaDTO = categoria.ToCategoriaDTO();
            return Ok(categoriaDTO);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> CadastraCategoria(CategoriaDTO dto)
        {
            if (dto == null) return BadRequest("Dados enviados são inválidos!");

            var categoria = dto.ToCategoria();
            var categoriaCriada = _ufw.CategoriaRepository.Create(categoria);
            _ufw.Commit();

            var categoriaDTO = categoriaCriada.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaDTO?.CategoriaId }, categoriaDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> AlteraCategoria(int id, CategoriaDTO dto)
        {
            if (id != dto.CategoriaId)
            {
                return BadRequest("Dados enviados são inválidos!");
            }

            var categoria = dto.ToCategoria();
            var categoriaAlterada = _ufw.CategoriaRepository.Update(categoria);
            _ufw.Commit();

            var categoriaDTO = categoriaAlterada.ToCategoriaDTO();
            return Ok(categoriaDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> ExcluiCategoria(int id) 
        {
            var categoria = _ufw.CategoriaRepository.Get(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound($"Categoria com id = {id} não encontrada...");
            }

            var categoriaExcluida = _ufw.CategoriaRepository.Delete(categoria);
            _ufw.Commit();

            var categoriaDTO = categoriaExcluida.ToCategoriaDTO();
            return Ok(categoriaExcluida);
        }
    }
}
