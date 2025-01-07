using System.Collections.Generic;
using System.Threading.Tasks; // Importando o namespace para operações assíncronas
using Modelo.Domain.Models;

namespace Modelo.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task InserirAutorAsync(AutorRequestModel autor); // Alterando para método assíncrono
        Task AtualizarAutorAsync(int codAu, AutorRequestModel autor); // Alterando para método assíncrono
        Task DeletarAutorAsync(int codAu); // Alterando para método assíncrono
        Task<AutorResponseModel> ObterAutorPorIdAsync(int codAu); // Alterando para método assíncrono
        Task<IEnumerable<AutorResponseModel>> ListarAutoresAsync(); // Alterando para método assíncrono
    }
}
