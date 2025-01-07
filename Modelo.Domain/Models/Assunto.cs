namespace Modelo.Domain.Models
{
    public class Assunto
    {
        public int CodAssunto { get; set; }
        public string Descricao { get; set; } = string.Empty;
        //public List<Livro> Livros { get; set; }  // Atualizado para lista de livros
    }

}
