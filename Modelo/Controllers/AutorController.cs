using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;

namespace Modelo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpPost]
        public IActionResult InserirAutor([FromBody] Autor autor)
        {
            try
            {
                _autorService.InserirAutor(autor);
                return Ok(new { Message = "Autor inserido com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao inserir o autor: {ex.Message}" });
            }
        }

        [HttpPut("{codAu}")]
        public IActionResult AtualizarAutor(int codAu, [FromBody] Autor autor)
        {
            try
            {
                _autorService.AtualizarAutor(codAu, autor);
                return Ok(new { Message = "Autor atualizado com sucesso!" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Autor não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao atualizar o autor: {ex.Message}" });
            }
        }

        [HttpDelete("{codAu}")]
        public IActionResult DeletarAutor(int codAu)
        {
            try
            {
                _autorService.DeletarAutor(codAu);
                return Ok(new { Message = "Autor deletado com sucesso!" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Autor não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao deletar o autor: {ex.Message}" });
            }
        }

        [HttpGet("{codAu}")]
        public IActionResult ObterAutorPorId(int codAu)
        {
            try
            {
                var autor = _autorService.ObterAutorPorId(codAu);
                if (autor == null)
                {
                    return NotFound(new { Message = "Autor não encontrado." });
                }
                return Ok(autor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao obter os detalhes do autor: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult ListarAutores()
        {
            try
            {
                var autores = _autorService.ListarAutores();
                return Ok(autores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao listar os autores: {ex.Message}" });
            }
        }
    }
}
