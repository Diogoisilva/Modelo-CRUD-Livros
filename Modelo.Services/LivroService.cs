using Modelo.Domain.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class LivroService : ILivroService
{
    public async Task<IEnumerable<Livro>> GetLivrosAsync(string connectionString)
    {
        var livros = new List<Livro>();

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spGetLivros", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    livros.Add(new Livro
                    {
                        CodL = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Editora = reader.GetString(2),
                        Edicao = reader.GetString(3),
                        AnoPublicacao = reader.GetString(4),
                        Preco = reader.GetDecimal(5),
                        FormaCompra = reader.GetString(6),
                        Autor = reader.IsDBNull(7) ? null : new Autor { CodAu = reader.GetInt32(7), Nome = reader.GetString(8) },
                        Assunto = reader.IsDBNull(9) ? null : new Assunto { CodAssunto = reader.GetInt32(9), Descricao = reader.GetString(10) }
                    });
                }
            }
        }

        return livros;
    }

    public async Task<Livro> GetLivroByIdAsync(string connectionString, int id)
    {
        Livro livro = null;

        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spGetLivroById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", id);

            connection.Open();
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    livro = new Livro
                    {
                        CodL = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Editora = reader.GetString(2),
                        Edicao = reader.GetString(3),
                        AnoPublicacao = reader.GetString(4),
                        Preco = reader.GetDecimal(5),
                        FormaCompra = reader.GetString(6),
                        Autor = reader.IsDBNull(7) ? null : new Autor { CodAu = reader.GetInt32(7), Nome = reader.GetString(8) },
                        Assunto = reader.IsDBNull(9) ? null : new Assunto { CodAssunto = reader.GetInt32(9), Descricao = reader.GetString(10) }
                    };
                }
            }
        }

        return livro;
    }

    public async Task<Livro> CreateLivroAsync(string connectionString, Livro livro)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spCreateLivro", connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 30 // Definindo o tempo limite para 30 segundos
            };
            command.Parameters.AddWithValue("@Titulo", livro.Titulo);
            command.Parameters.AddWithValue("@Editora", livro.Editora);
            command.Parameters.AddWithValue("@Edicao", livro.Edicao);
            command.Parameters.AddWithValue("@AnoPublicacao", livro.AnoPublicacao);
            command.Parameters.AddWithValue("@Preco", livro.Preco);
            command.Parameters.AddWithValue("@FormaCompra", livro.FormaCompra);
            command.Parameters.AddWithValue("@CodAu", livro.Autor?.CodAu ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@CodAssunto", livro.Assunto?.CodAssunto ?? (object)DBNull.Value);

            connection.Open();
            livro.CodL = (int)await command.ExecuteScalarAsync();
        }

        return livro;
    }




    public async Task UpdateLivroAsync(string connectionString, Livro livro)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spUpdateLivro", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", livro.CodL);
            command.Parameters.AddWithValue("@Titulo", livro.Titulo);
            command.Parameters.AddWithValue("@Editora", livro.Editora);
            command.Parameters.AddWithValue("@Edicao", livro.Edicao);
            command.Parameters.AddWithValue("@AnoPublicacao", livro.AnoPublicacao);
            command.Parameters.AddWithValue("@Preco", livro.Preco);
            command.Parameters.AddWithValue("@FormaCompra", livro.FormaCompra);

            connection.Open();
            await command.ExecuteNonQueryAsync();

            if (livro.Autor != null)
            {
                await UpdateLivroAutorAsync(connectionString, livro.CodL, livro.Autor.CodAu);
            }

            if (livro.Assunto != null)
            {
                await UpdateLivroAssuntoAsync(connectionString, livro.CodL, livro.Assunto.CodAssunto);
            }
        }
    }

    public async Task DeleteLivroAsync(string connectionString, int id)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spDeleteLivro", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", id);

            connection.Open();
            await command.ExecuteNonQueryAsync();

            await DeleteLivroAutorAsync(connectionString, id);
            await DeleteLivroAssuntoAsync(connectionString, id);
        }
    }

    public async Task AddLivroAutorAsync(string connectionString, int codL, int codAu)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spCreateLivroAutor", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", codL);
            command.Parameters.AddWithValue("@CodAu", codAu);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }


    public async Task UpdateLivroAutorAsync(string connectionString, int codL, int codAu)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spUpdateLivroAutor", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", codL);
            command.Parameters.AddWithValue("@CodAu", codAu);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteLivroAutorAsync(string connectionString, int codL)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spDeleteLivroAutor", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", codL);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task AddLivroAssuntoAsync(string connectionString, int codL, int codAssunto)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spCreateLivroAssunto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", codL);
            command.Parameters.AddWithValue("@CodAssunto", codAssunto);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }


    public async Task UpdateLivroAssuntoAsync(string connectionString, int codL, int codAssunto)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spUpdateLivroAssunto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", codL);
            command.Parameters.AddWithValue("@CodAssunto", codAssunto);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteLivroAssuntoAsync(string connectionString, int codL)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            var command = new SqlCommand("spDeleteLivroAssunto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@CodL", codL);

            connection.Open();
            await command.ExecuteNonQueryAsync();
        }
    }
}
