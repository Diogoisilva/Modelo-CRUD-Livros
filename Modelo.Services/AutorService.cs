using Modelo.Domain.Interfaces;
using Modelo.Domain.Models;
using System.Threading.Tasks;

namespace Modelo.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task InserirAutorAsync(AutorRequestModel autor)
        {
            await _autorRepository.InserirAutorAsync(autor);
        }

        public async Task AtualizarAutorAsync(int codAu, AutorRequestModel autor)
        {
            await _autorRepository.AtualizarAutorAsync(codAu, autor);
        }

        public async Task DeletarAutorAsync(int codAu)
        {
            await _autorRepository.DeletarAutorAsync(codAu);
        }

        public async Task<AutorResponseModel> ObterAutorPorIdAsync(int codAu)
        {
            return await _autorRepository.ObterAutorPorIdAsync(codAu);
        }

        public async Task<IEnumerable<AutorResponseModel>> ListarAutoresAsync()
        {
            return await _autorRepository.ListarAutoresAsync();
        }
    }
}
