using Modelo.Domain.Models;
using System.Collections.Generic;

namespace Modelo.Domain.Interfaces
{
    public interface IAssuntoService
    {
        void InserirAssunto(AssuntoRequestModel assunto);
        void AtualizarAssunto(int codAssunto, AssuntoRequestModel assunto);
        void DeletarAssunto(int codAssunto);
        AssuntoResponseModel ObterAssuntoPorId(int codAssunto);
        IEnumerable<AssuntoResponseModel> ListarAssuntos();
    }
}
