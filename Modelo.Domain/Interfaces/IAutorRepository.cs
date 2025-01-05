using System.Collections.Generic;
using Modelo.Domain.Models;

namespace Modelo.Domain.Interfaces
{
    public interface IAutorRepository
    {
        void InserirAutor(AutorRequestModel autor);
        void AtualizarAutor(int codAu, AutorRequestModel autor);
        void DeletarAutor(int codAu);
        AutorResponseModel ObterAutorPorId(int codAu);
        IEnumerable<AutorResponseModel> ListarAutores();
    }
}
