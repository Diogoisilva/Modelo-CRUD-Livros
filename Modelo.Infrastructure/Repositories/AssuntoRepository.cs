using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infrastructure.Data;

namespace Modelo.Infrastructure.Repositories
{
    public class AssuntoRepository : IAssuntoRepository
    {
        private readonly DbConnectionHelper _dbConnectionHelper;

        public AssuntoRepository(DbConnectionHelper dbConnectionHelper)
        {
            _dbConnectionHelper = dbConnectionHelper;
        }

        public void InserirAssunto(AssuntoRequestModel assunto)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("InserirAssunto", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Descricao", assunto.Descricao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao inserir o assunto. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao inserir o assunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public void AtualizarAssunto(int codAssunto, AssuntoRequestModel assunto)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("AtualizarAssunto", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@CodAssunto", codAssunto);
                    command.Parameters.AddWithValue("@Descricao", assunto.Descricao);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao atualizar o assunto. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao atualizar o assunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public void DeletarAssunto(int codAssunto)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("DeletarAssunto", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@CodAssunto", codAssunto);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao deletar o assunto. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao deletar o assunto. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public AssuntoResponseModel ObterAssuntoPorId(int codAssunto)
        {
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("ObterAssuntoPorId", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@CodAssunto", codAssunto);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AssuntoResponseModel
                            {
                                CodAssunto = (int)reader["CodAs"],
                                Descricao = (string)reader["Descricao"]
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao obter o assunto. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao obter o assunto. Por favor, tente novamente mais tarde.", ex);
            }

            return null;
        }

        public IEnumerable<AssuntoResponseModel> ListarAssuntos()
        {
            var assuntos = new List<AssuntoResponseModel>();
            try
            {
                using (var connection = _dbConnectionHelper.GetConnection())
                {
                    var command = new SqlCommand("ListarAssuntos", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assuntos.Add(new AssuntoResponseModel
                            {
                                CodAssunto = (int)reader["CodAs"],
                                Descricao = (string)reader["Descricao"]
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Ocorreu um erro no banco de dados ao listar os assuntos. Por favor, tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro inesperado ao listar os assuntos. Por favor, tente novamente mais tarde.", ex);
            }

            return assuntos;
        }
    }
}
