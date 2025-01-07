using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Domain.Models
{
    public class RelatorioLivro
    {
        public int CodL { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public decimal Preco { get; set; }
        public string FormaCompra { get; set; }
        public string AutorNome { get; set; }
        public string AssuntoDescricao { get; set; }
    }


}
