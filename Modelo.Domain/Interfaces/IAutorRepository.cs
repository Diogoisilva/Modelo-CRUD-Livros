using Modelo.Domain.Models;
using System.Collections.Generic;

namespace Modelo.Domain.Interfaces
{
    public interface IAutorRepository
    {
        void InserirAutor(Autor autor);
        void AtualizarAutor(int codAu, Autor autor);
        void DeletarAutor(int codAu);
        Autor ObterAutorPorId(int codAu);
        IEnumerable<Autor> ListarAutores();
    }
}
