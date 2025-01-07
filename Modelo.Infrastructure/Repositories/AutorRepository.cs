using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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

        public async Task InserirAutorAsync(AutorRequestModel autor)
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
                    await command.ExecuteNonQueryAsync();
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

        public async Task AtualizarAutorAsync(int codAu, AutorRequestModel autor)
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
                    await command.ExecuteNonQueryAsync();
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

        public async Task DeletarAutorAsync(int codAu)
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
                    await command.ExecuteNonQueryAsync();
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

        public async Task<AutorResponseModel> ObterAutorPorIdAsync(int codAu)
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
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
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

        public async Task<IEnumerable<AutorResponseModel>> ListarAutoresAsync()
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
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
