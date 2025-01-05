using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infrastructure.Data;

namespace Modelo.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DbConnectionHelper _dbConnectionHelper;

        public LivroRepository(DbConnectionHelper dbConnectionHelper)
        {
            _dbConnectionHelper = dbConnectionHelper;
        }

        public void InserirLivro(LivroRequestModel livro)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("InserirLivro", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Titulo", livro.Titulo);
                    command.Parameters.AddWithValue("@Editora", livro.Editora);
                    command.Parameters.AddWithValue("@Edicao", livro.Edicao);
                    command.Parameters.AddWithValue("@AnoPublicacao", livro.AnoPublicacao);
                    command.Parameters.AddWithValue("@Preco", livro.Preco);
                    command.Parameters.AddWithValue("@FormaCompra", livro.FormaCompra);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao inserir o livro. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao inserir o livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public void AtualizarLivro(int codl, LivroRequestModel livro)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("AtualizarLivro", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Codl", codl);
                    command.Parameters.AddWithValue("@Titulo", livro.Titulo);
                    command.Parameters.AddWithValue("@Editora", livro.Editora);
                    command.Parameters.AddWithValue("@Edicao", livro.Edicao);
                    command.Parameters.AddWithValue("@AnoPublicacao", livro.AnoPublicacao);
                    command.Parameters.AddWithValue("@Preco", livro.Preco);
                    command.Parameters.AddWithValue("@FormaCompra", livro.FormaCompra);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao atualizar o livro. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar o livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public void DeletarLivro(int codl)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("DeletarLivro", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Codl", codl);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao deletar o livro. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao deletar o livro. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public LivroResponseModel ObterLivroPorId(int codl)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("ObterLivroPorId", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Codl", codl);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LivroResponseModel
                            {
                                Codl = (int)reader["Codl"],
                                Titulo = (string)reader["Titulo"],
                                Editora = (string)reader["Editora"],
                                Edicao = (string)reader["Edicao"],
                                AnoPublicacao = (string)reader["AnoPublicacao"],
                                Preco = (decimal)reader["Preco"],
                                FormaCompra = (string)reader["FormaCompra"]
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao obter o livro. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao obter o livro. Por favor, tente novamente mais tarde.", ex);
            }

            return null;
        }

        public IEnumerable<LivroResponseModel> ListarLivros()
        {
            var livros = new List<LivroResponseModel>();
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("ListarLivros", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            livros.Add(new LivroResponseModel
                            {
                                Codl = (int)reader["Codl"],
                                Titulo = (string)reader["Titulo"],
                                Editora = (string)reader["Editora"],
                                Edicao = (string)reader["Edicao"],
                                AnoPublicacao = (string)reader["AnoPublicacao"],
                                Preco = (decimal)reader["Preco"],
                                FormaCompra = (string)reader["FormaCompra"]
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao listar os livros. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao listar os livros. Por favor, tente novamente mais tarde.", ex);
            }

            return livros;
        }
    }
}
