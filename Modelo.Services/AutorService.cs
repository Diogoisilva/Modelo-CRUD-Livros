using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using Modelo.Cross.Helpers;
using System.Collections.Generic;

namespace Modelo.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public void InserirAutor(Autor autor)
        {
            ValidationHelper.ValidateString(autor.Nome, "Nome");
            _autorRepository.InserirAutor(autor);
        }

        public void AtualizarAutor(int codAu, Autor autor)
        {
            var autorExistente = _autorRepository.ObterAutorPorId(codAu);
            if (autorExistente == null)
            {
                throw new KeyNotFoundException("Autor não encontrado.");
            }

            _autorRepository.AtualizarAutor(codAu, autor);
        }

        public void DeletarAutor(int codAu)
        {
            _autorRepository.DeletarAutor(codAu);
        }

        public Autor ObterAutorPorId(int codAu)
        {
            return _autorRepository.ObterAutorPorId(codAu);
        }

        public IEnumerable<Autor> ListarAutores()
        {
            return _autorRepository.ListarAutores();
        }
    }
}
