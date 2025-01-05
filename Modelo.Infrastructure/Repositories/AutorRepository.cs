using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infrastructure.Data;

namespace Modelo.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly string _connectionString;

        public AutorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InserirAutor(Autor autor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Autor (Nome) VALUES (@Nome)", connection);
                command.Parameters.AddWithValue("@Nome", autor.Nome);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AtualizarAutor(int codAu, Autor autor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Autor SET Nome = @Nome WHERE CodAu = @CodAu", connection);
                command.Parameters.AddWithValue("@Nome", autor.Nome);
                command.Parameters.AddWithValue("@CodAu", codAu);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeletarAutor(int codAu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Autor WHERE CodAu = @CodAu", connection);
                command.Parameters.AddWithValue("@CodAu", codAu);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Autor ObterAutorPorId(int codAu)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Autor WHERE CodAu = @CodAu", connection);
                command.Parameters.AddWithValue("@CodAu", codAu);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Autor
                        {
                            CodAu = (int)reader["CodAu"],
                            Nome = (string)reader["Nome"]
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<Autor> ListarAutores()
        {
            var autores = new List<Autor>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Autor", connection);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        autores.Add(new Autor
                        {
                            CodAu = (int)reader["CodAu"],
                            Nome = (string)reader["Nome"]
                        });
                    }
                }
            }
            return autores;
        }
    }
}
