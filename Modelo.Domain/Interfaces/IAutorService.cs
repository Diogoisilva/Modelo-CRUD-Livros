using Modelo.Domain.Models;

namespace Modelo.Domain.Interfaces
{
    public interface IAutorService
    {
        void InserirAutor(Autor autor);
        void AtualizarAutor(int codAu, Autor autor);
        void DeletarAutor(int codAu);
        Autor ObterAutorPorId(int codAu);
        IEnumerable<Autor> ListarAutores();
    }
}
