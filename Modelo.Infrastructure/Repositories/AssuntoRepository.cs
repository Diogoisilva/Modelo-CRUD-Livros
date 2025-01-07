using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Infrastructure.Data;
using Modelo.Infrastructure.Exceptions;
using System.Data.SqlClient;
using System.Data;

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
            using (var connection = _dbConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("InserirAssunto", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Descricao", assunto.Descricao);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("Erro ao inserir o assunto.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao inserir o assunto.", ex);
                }
            }
        }

        public void AtualizarAssunto(int codAssunto, AssuntoRequestModel assunto)
        {
            using (var connection = _dbConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("AtualizarAssunto", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CodAssunto", codAssunto);
                command.Parameters.AddWithValue("@Descricao", assunto.Descricao);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("Erro ao atualizar o assunto.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao atualizar o assunto.", ex);
                }
            }
        }

        public void DeletarAssunto(int codAssunto)
        {
            using (var connection = _dbConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("DeletarAssunto", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CodAssunto", codAssunto);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("Erro ao deletar o assunto.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao deletar o assunto.", ex);
                }
            }
        }

        public AssuntoResponseModel ObterAssuntoPorId(int codAssunto)
        {
            using (var connection = _dbConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("ObterAssuntoPorId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@CodAssunto", codAssunto);

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AssuntoResponseModel
                            {
                                CodAssunto = Convert.ToInt32(reader["CodAs"]),
                                Descricao = reader["Descricao"].ToString()
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("Erro ao obter o assunto.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao obter o assunto.", ex);
                }
            }

            return null;
        }

        public IEnumerable<AssuntoResponseModel> ListarAssuntos()
        {
            var assuntos = new List<AssuntoResponseModel>();

            using (var connection = _dbConnectionHelper.GetConnection())
            {
                var command = new SqlCommand("ListarAssuntos", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                try
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assuntos.Add(new AssuntoResponseModel
                            {
                                CodAssunto = (int)reader["CodAs"],
                                Descricao = reader["Descricao"] as string
                            });
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("Erro ao listar os assuntos.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro inesperado ao listar os assuntos.", ex);
                }
            }

            return assuntos;
        }
    }
}
