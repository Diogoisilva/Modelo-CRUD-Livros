using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Cross.Helpers;
using System.Collections.Generic;

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
            ValidationHelper.ValidateString(assunto.Descricao, "Descricao");
            _assuntoRepository.InserirAssunto(assunto);
        }

        public void AtualizarAssunto(int codAssunto, AssuntoRequestModel assunto)
        {
            var assuntoExistente = _assuntoRepository.ObterAssuntoPorId(codAssunto);
            if (assuntoExistente == null)
            {
                throw new KeyNotFoundException("Assunto não encontrado.");
            }

            _assuntoRepository.AtualizarAssunto(codAssunto, assunto);
        }

        public void DeletarAssunto(int codAssunto)
        {
            _assuntoRepository.DeletarAssunto(codAssunto);
        }

        public AssuntoResponseModel ObterAssuntoPorId(int codAssunto)
        {
            return _assuntoRepository.ObterAssuntoPorId(codAssunto);
        }

        public IEnumerable<AssuntoResponseModel> ListarAssuntos()
        {
            return _assuntoRepository.ListarAssuntos();
        }
    }
}
