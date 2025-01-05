using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;

namespace Modelo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssuntoController : ControllerBase
    {
        private readonly IAssuntoService _assuntoService;

        public AssuntoController(IAssuntoService assuntoService)
        {
            _assuntoService = assuntoService;
        }

        [HttpPost]
        public IActionResult InserirAssunto([FromBody] AssuntoRequestModel assunto)
        {
            try
            {
                _assuntoService.InserirAssunto(assunto);
                return Ok(new { Message = "Assunto inserido com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao inserir o assunto: {ex.Message}" });
            }
        }

        [HttpPut("{codAssunto}")]
        public IActionResult AtualizarAssunto(int codAssunto, [FromBody] AssuntoRequestModel assunto)
        {
            try
            {
                _assuntoService.AtualizarAssunto(codAssunto, assunto);
                return Ok(new { Message = "Assunto atualizado com sucesso!" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Assunto não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao atualizar o assunto: {ex.Message}" });
            }
        }

        [HttpDelete("{codAssunto}")]
        public IActionResult DeletarAssunto(int codAssunto)
        {
            try
            {
                _assuntoService.DeletarAssunto(codAssunto);
                return Ok(new { Message = "Assunto deletado com sucesso!" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Message = "Assunto não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao deletar o assunto: {ex.Message}" });
            }
        }

        [HttpGet("{codAssunto}")]
        public IActionResult ObterAssuntoPorId(int codAssunto)
        {
            try
            {
                var assunto = _assuntoService.ObterAssuntoPorId(codAssunto);
                if (assunto == null)
                {
                    return NotFound(new { Message = "Assunto não encontrado." });
                }
                return Ok(assunto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao obter os detalhes do assunto: {ex.Message}" });
            }
        }

        [HttpGet]
        public IActionResult ListarAssuntos()
        {
            try
            {
                var assuntos = _assuntoService.ListarAssuntos();
                return Ok(assuntos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Erro ao listar os assuntos: {ex.Message}" });
            }
        }
    }
}
