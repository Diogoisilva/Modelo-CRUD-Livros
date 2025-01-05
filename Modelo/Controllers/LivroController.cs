using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Interfaces;
using Modelo.Api.Models;

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
            return Ok();
        }

        [HttpPut("{codl}")]
        public IActionResult AtualizarLivro(int codl, [FromBody] LivroRequestModel livro)
        {
            _livroService.AtualizarLivro(codl, livro);
            return Ok();
        }

        [HttpDelete("{codl}")]
        public IActionResult DeletarLivro(int codl)
        {
            _livroService.DeletarLivro(codl);
            return Ok();
        }

        [HttpGet("{codl}")]
        public IActionResult ObterLivroPorId(int codl)
        {
            var livro = _livroService.ObterLivroPorId(codl);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpGet]
        public IActionResult ListarLivros()
        {
            var livros = _livroService.ListarLivros();
            return Ok(livros);
        }
    }
}
