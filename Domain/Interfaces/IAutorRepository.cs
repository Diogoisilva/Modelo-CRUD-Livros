using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface IAutorRepository
    {
        void Inserir(Autor autor);
        IEnumerable<Autor> Listar();
        Autor BuscarPorId(int id);
        void Atualizar(Autor autor);
        void Excluir(int id);
    }
}
