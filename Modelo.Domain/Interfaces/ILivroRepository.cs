using Modelo.Domain.Models;

namespace Modelo.Domain.Interfaces
{
    public interface ILivroRepository
    {
        void InserirLivro(LivroRequestModel livro);
        void AtualizarLivro(int codl, LivroRequestModel livro);
        void DeletarLivro(int codl);
        LivroResponseModel ObterLivroPorId(int codl);
        IEnumerable<LivroResponseModel> ListarLivros();
    }
}
