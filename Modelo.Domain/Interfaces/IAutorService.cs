using Modelo.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface IAutorService
    {
        Task InserirAutorAsync(AutorRequestModel autor);
        Task AtualizarAutorAsync(int codAu, AutorRequestModel autor);
        Task DeletarAutorAsync(int codAu);
        Task<AutorResponseModel> ObterAutorPorIdAsync(int codAu);
        Task<IEnumerable<AutorResponseModel>> ListarAutoresAsync();
    }
}