using Modelo.Domain.Models;
using System.Collections.Generic;

namespace Modelo.Domain.Interfaces
{
    public interface IAutorService
    {
        void InserirAutor(AutorRequestModel autor);
        void AtualizarAutor(int codAu, AutorRequestModel autor);
        void DeletarAutor(int codAu);
        AutorResponseModel ObterAutorPorId(int codAu);
        IEnumerable<AutorResponseModel> ListarAutores();
    }
}
