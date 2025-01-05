using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;

namespace Modelo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public IActionResult InserirLivro([FromBody] LivroRequestModel livro)
        {
            _livroService.InserirLivro(livro);
            return Ok(new { Message = "Livro inserido com sucesso!" });
        }

        [HttpPut("{codl}")]
        public IActionResult AtualizarLivro(int codl, [FromBody] LivroRequestModel livro)
        {
            try
            {
                _livroService.AtualizarLivro(codl, livro);
                return Ok(new { Message = "Livro atualizado com sucesso!" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Livro não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao atualizar o livro: {ex.Message}" });
            }
        }

        [HttpDelete("{codl}")]
        public IActionResult DeletarLivro(int codl)
        {
            try
            {
                _livroService.DeletarLivro(codl);
                return Ok(new { Message = "Livro deletado com sucesso!" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Livro não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao deletar o livro: {ex.Message}" });
            }
        }

        [HttpGet("{codl}")]
        public IActionResult ObterLivroPorId(int codl)
        {
            try
            {
                var livro = _livroService.ObterLivroPorId(codl);
                if (livro == null)
                {
                    return NotFound(new { Message = "Livro não encontrado." });
                }
                return Ok(livro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao obter os detalhes do livro: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult ListarLivros()
        {
            try
            {
                var livros = _livroService.ListarLivros();
                return Ok(livros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao listar os livros: {ex.Message}" });
            }
        }
    }
}
