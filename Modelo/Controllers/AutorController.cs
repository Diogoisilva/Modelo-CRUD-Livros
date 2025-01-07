using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using System.Threading.Tasks;

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
        public async Task<IActionResult> InserirAutor([FromBody] AutorRequestModel autor)
        {
            try
            {
                await _autorService.InserirAutorAsync(autor);
                return Ok(new { Message = "Autor inserido com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao inserir o autor: {ex.Message}" });
            }
        }

        [HttpPut("{codAu}")]
        public async Task<IActionResult> AtualizarAutor(int codAu, [FromBody] AutorRequestModel autor)
        {
            try
            {
                await _autorService.AtualizarAutorAsync(codAu, autor);
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
        public async Task<IActionResult> DeletarAutor(int codAu)
        {
            try
            {
                await _autorService.DeletarAutorAsync(codAu);
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
        public async Task<IActionResult> ObterAutorPorId(int codAu)
        {
            try
            {
                var autor = await _autorService.ObterAutorPorIdAsync(codAu);
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
        public async Task<IActionResult> ListarAutores()
        {
            try
            {
                var autores = await _autorService.ListarAutoresAsync();
                return Ok(autores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao listar os autores: {ex.Message}" });
            }
        }
    }
}
