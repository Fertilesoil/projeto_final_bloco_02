
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using projeto_final_bloco_02.Models;
using projeto_final_bloco_02.Service;

namespace projeto_final_bloco_02.Controllers
{
    [Route("~/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProdutoService produtoService, IValidator<Produto> produtoValidator)
        {
            _produtoService = produtoService;
            _produtoValidator = produtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult> ListarTodosProdutos()
        {
            return Ok(await _produtoService.ListarTodosProdutos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> BuscarPorId(long id)
        {
            var Resposta = await  _produtoService.BuscarPorId(id);

            if (Resposta is null)
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult> BuscarPorNome(string nome)
        {
            return Ok(await _produtoService.BuscarPorNome(nome));
        }

        [HttpPost]
        public async Task<ActionResult> Criar([FromBody] Produto produto)
        {
            var ValidarProduto = await  _produtoValidator.ValidateAsync(produto);

            if (!ValidarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ValidarProduto);

                var Resposta = await _produtoService.Criar(produto);

                if (Resposta is null)
                return BadRequest("Categoria não encontrada!");

            return CreatedAtAction(nameof(BuscarPorId), new { id = produto.Id }, produto);
        }

        [HttpPut]
        public async Task<ActionResult> Atualizar([FromBody] Produto produto)
        {
            if (produto.Id == 0)
                return BadRequest("Id do Produto é Invalido");

            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var Resposta = await _produtoService.Atualizar(produto);

            if (Resposta is null)
                return NotFound("Produto ou Categoria Não Encontrados !!");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            var BuscaProduto = await _produtoService.BuscarPorId(id);

            if (BuscaProduto is null)
                return NotFound("Produto não Encontrado.");

            await _produtoService.Deletar(BuscaProduto);

            return NoContent();
        }
    }
}