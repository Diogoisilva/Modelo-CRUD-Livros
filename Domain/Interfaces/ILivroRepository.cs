using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface ILivroRepository
    {
        void Inserir(Livro livro);
        IEnumerable<Livro> Listar();
        Livro BuscarPorId(int id);
        void Atualizar(Livro livro);
        void Excluir(int id);
    }
