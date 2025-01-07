using Microsoft.AspNetCore.Mvc;

using Modelo.Domain.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class RelatorioController : ControllerBase
{
    private readonly string _connectionString;

    public RelatorioController(IConfiguration configuration)
    {
        // Obtendo a string de conexão a partir do arquivo de configurações
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // Endpoint para obter o relatório de livros
    [HttpGet("relatoriolivros")]
    public async Task<IActionResult> GetRelatorioLivros()
    {
        // Criando a lista que irá armazenar o relatório
        List<RelatorioLivro> relatorio = new List<RelatorioLivro>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            // Criando o comando SQL
            string query = "SELECT * FROM vw_LivroRelatorio";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // Abrindo a conexão com o banco de dados
                await connection.OpenAsync();

                // Executando a consulta e obtendo os resultados
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Lendo os dados da consulta e preenchendo o objeto RelatorioLivro
                        var item = new RelatorioLivro
                        {
                            CodL = reader.GetInt32(reader.GetOrdinal("LivroId")),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Editora = reader.GetString(reader.GetOrdinal("Editora")),
                            Edicao = reader.GetString(reader.GetOrdinal("Edicao")),
                            AnoPublicacao = reader.GetString(reader.GetOrdinal("AnoPublicacao")),
                            Preco = reader.GetDecimal(reader.GetOrdinal("Preco")),
                            FormaCompra = reader.GetString(reader.GetOrdinal("FormaCompra")),
                            AutorNome = reader.GetString(reader.GetOrdinal("AutorNome")),
                            AssuntoDescricao = reader.GetString(reader.GetOrdinal("AssuntoDescricao"))
                        };
                        relatorio.Add(item);
                    }
                }
            }
        }

        // Retornando o relatório como resultado da API
        return Ok(relatorio);
    }
}
