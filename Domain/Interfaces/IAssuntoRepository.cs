using Modelo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Interfaces
{
    public interface IAssuntoRepository
    {
        void Inserir(Assunto assunto);
        IEnumerable<Assunto> Listar();
        Assunto BuscarPorId(int id);
        void Atualizar(Assunto assunto);
        void Excluir(int id);
    }
}
