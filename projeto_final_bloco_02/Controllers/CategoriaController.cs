using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using projeto_final_bloco_02.Models;
using projeto_final_bloco_02.Service;

namespace projeto_final_bloco_02.Controllers
{
    [Route("~/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IValidator<Categoria> _categoriaValidator;
        public CategoriaController(
            ICategoriaService categoriaService,
            IValidator<Categoria> categoriaValidator)
        {
            _categoriaService = categoriaService;
            _categoriaValidator = categoriaValidator;
        }

        [HttpGet]
        public async Task<ActionResult> ListarTodasCategorias()
        {
            return Ok(await _categoriaService.ListarTodasCategorias());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarPorId(long id)
        {
            var Resposta = await _categoriaService.BuscarPorId(id);

            if (Resposta is null)
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("setor/{setor}")]
        public async Task<ActionResult> BuscarPorSetor(string setor)
        {
            return Ok(await _categoriaService.BuscarPorSetor(setor));
        }

        [HttpPost]
        public async Task<ActionResult> Criar([FromBody] Categoria categoria)
        {
            var ValidarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!ValidarCategoria.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ValidarCategoria);

            var Resposta = await _categoriaService.Criar(categoria);

            if (Resposta is null)
                return BadRequest("Categoria Não Encontrada!");

            return CreatedAtAction(nameof(BuscarPorId), new { id = categoria.Id }, categoria);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar([FromBody] Categoria categoria)
        {
            if (categoria.Id == 0)
                return BadRequest("Id da Categoria é Invalido");

            var ValidarCategoria = await _categoriaValidator.ValidateAsync(categoria);

            if (!ValidarCategoria.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ValidarCategoria);

            var Resposta = await _categoriaService.Atualizar(categoria);

            if (Resposta is null)
                return NotFound("Categoria Não Encontrada");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            var BuscaCategoria = await _categoriaService.BuscarPorId(id);

            if (BuscaCategoria is null)
                return NotFound("Categoria não Encontrada.");

            await _categoriaService.Deletar(BuscaCategoria);

            return NoContent();
        }
    }
}