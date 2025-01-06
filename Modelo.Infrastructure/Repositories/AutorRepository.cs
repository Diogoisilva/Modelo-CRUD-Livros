using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infrastructure.Data;

namespace Modelo.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly DbConnectionHelper _dbConnectionHelper;

        public AutorRepository(DbConnectionHelper dbConnectionHelper)
        {
            _dbConnectionHelper = dbConnectionHelper;
        }

        public void InserirAutor(AutorRequestModel autor)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("InserirAutor", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Nome", autor.Nome);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao inserir o autor. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao inserir o autor. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public void AtualizarAutor(int codAu, AutorRequestModel autor)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("AtualizarAutor", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@CodAu", codAu);
                    command.Parameters.AddWithValue("@Nome", autor.Nome);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao atualizar o autor. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar o autor. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public void DeletarAutor(int codAu)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("DeletarAutor", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@CodAu", codAu);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao deletar o autor. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao deletar o autor. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public AutorResponseModel ObterAutorPorId(int codAu)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("ObterAutorPorId", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@CodAu", codAu);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AutorResponseModel
                            {
                                CodAu = (int)reader["CodAu"],
                                Nome = (string)reader["Nome"]
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao obter o autor. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao obter o autor. Por favor, tente novamente mais tarde.", ex);
            }

            return null;
        }

        public IEnumerable<AutorResponseModel> ListarAutores()
        {
            var autores = new List<AutorResponseModel>();
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("ListarAutores", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            autores.Add(new AutorResponseModel
                            {
                                CodAu = (int)reader["CodAu"],
                                Nome = (string)reader["Nome"]
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao listar os autores. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao listar os autores. Por favor, tente novamente mais tarde.", ex);
            }

            return autores;
        }
    }
}
