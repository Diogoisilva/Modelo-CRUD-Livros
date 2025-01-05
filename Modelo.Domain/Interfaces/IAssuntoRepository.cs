using System.Collections.Generic;
using Modelo.Domain.Models;

namespace Modelo.Domain.Interfaces
{
    public interface IAssuntoRepository
    {
        void InserirAssunto(AssuntoRequestModel assunto);
        void AtualizarAssunto(int codAssunto, AssuntoRequestModel assunto);
        void DeletarAssunto(int codAssunto);
        AssuntoResponseModel ObterAssuntoPorId(int codAssunto);
        IEnumerable<AssuntoResponseModel> ListarAssuntos();
    }
}
