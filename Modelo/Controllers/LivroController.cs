﻿using Microsoft.AspNetCore.Mvc;
using Modelo.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;
    private readonly string _connectionString;

    public LivroController(ILivroService livroService, IConfiguration configuration)
    {
        _livroService = livroService;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
    {
        var livros = await _livroService.GetLivrosAsync(_connectionString);
        return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Livro>> GetLivro(int id)
    {
        var livro = await _livroService.GetLivroByIdAsync(_connectionString, id);
        if (livro == null)
        {
            return NotFound();
        }
        return Ok(livro);
    }

    [HttpPost]
    public async Task<ActionResult<Livro>> CreateLivro([FromBody] Livro livro)
    {
        try
        {
            var createdLivro = await _livroService.CreateLivroAsync(_connectionString, livro);
            return CreatedAtAction(nameof(GetLivro), new { id = createdLivro.CodL }, createdLivro);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Erro ao criar o livro: {ex.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLivro(int id, [FromBody] Livro livro)
    {
        if (id != livro.CodL)
        {
            return BadRequest();
        }

        try
        {
            await _livroService.UpdateLivroAsync(_connectionString, livro);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Erro ao atualizar o livro: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLivro(int id)
    {
        try
        {
            await _livroService.DeleteLivroAsync(_connectionString, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Erro ao excluir o livro: {ex.Message}" });
        }
    }
}
