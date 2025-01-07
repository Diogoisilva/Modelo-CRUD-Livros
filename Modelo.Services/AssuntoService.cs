using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Cross.Helpers;
using Modelo.Infrastructure.Exceptions;
using System.Collections.Generic;
using System;

namespace Modelo.Services
{
    public class AssuntoService : IAssuntoService
    {
        private readonly IAssuntoRepository _assuntoRepository;

        public AssuntoService(IAssuntoRepository assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public void InserirAssunto(AssuntoRequestModel assunto)
        {
            try
            {
                ValidationHelper.ValidateString(assunto.Descricao, "Descricao");
                _assuntoRepository.InserirAssunto(assunto);
            }
            catch (DatabaseException dbEx)
            {
                // Trate erro relacionado ao banco de dados
                throw new Exception("Erro ao inserir assunto. Tente novamente mais tarde.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao inserir assunto.", ex);
            }
        }

        public void AtualizarAssunto(int codAssunto, AssuntoRequestModel assunto)
        {
            try
            {
                var assuntoExistente = _assuntoRepository.ObterAssuntoPorId(codAssunto);
                if (assuntoExistente == null)
                {
                    throw new KeyNotFoundException("Assunto não encontrado.");
                }

                ValidationHelper.ValidateString(assunto.Descricao, "Descricao");
                _assuntoRepository.AtualizarAssunto(codAssunto, assunto);
            }
            catch (DatabaseException dbEx)
            {
                throw new Exception("Erro ao atualizar assunto. Tente novamente mais tarde.", dbEx);
            }
            catch (KeyNotFoundException knfEx)
            {
                throw knfEx; // Deixa a exceção original passar
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao atualizar assunto.", ex);
            }
        }

        public void DeletarAssunto(int codAssunto)
        {
            try
            {
                _assuntoRepository.DeletarAssunto(codAssunto);
            }
            catch (DatabaseException dbEx)
            {
                throw new Exception("Erro ao deletar assunto. Tente novamente mais tarde.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao deletar assunto.", ex);
            }
        }

        public AssuntoResponseModel ObterAssuntoPorId(int codAssunto)
        {
            try
            {
                return _assuntoRepository.ObterAssuntoPorId(codAssunto);
            }
            catch (DatabaseException dbEx)
            {
                throw new Exception("Erro ao obter assunto. Tente novamente mais tarde.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao obter assunto.", ex);
            }
        }

        public IEnumerable<AssuntoResponseModel> ListarAssuntos()
        {
            try
            {
                return _assuntoRepository.ListarAssuntos();
            }
            catch (DatabaseException dbEx)
            {
                throw new Exception("Erro ao listar assuntos. Tente novamente mais tarde.", dbEx);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro inesperado ao listar assuntos.", ex);
            }
        }

    }
}
