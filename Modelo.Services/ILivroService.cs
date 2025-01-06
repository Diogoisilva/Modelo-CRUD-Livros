using Modelo.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILivroService
{
    Task<IEnumerable<Livro>> GetLivrosAsync(string connectionString);
    Task<Livro> GetLivroByIdAsync(string connectionString, int id);
    Task<Livro> CreateLivroAsync(string connectionString, Livro livro);
    Task UpdateLivroAsync(string connectionString, Livro livro);
    Task DeleteLivroAsync(string connectionString, int id);

    Task AddLivroAutorAsync(string connectionString, int codL, int codAu);
    Task UpdateLivroAutorAsync(string connectionString, int codL, int codAu);
    Task DeleteLivroAutorAsync(string connectionString, int codL);

    Task AddLivroAssuntoAsync(string connectionString, int codL, int codAs);
    Task UpdateLivroAssuntoAsync(string connectionString, int codL, int codAs);
    Task DeleteLivroAssuntoAsync(string connectionString, int codL);
}
