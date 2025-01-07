using Modelo.Domain.Models;

public class Livro
{
    public int CodL { get; set; }
    public string Titulo { get; set; }
    public string Editora { get; set; }
    public string Edicao { get; set; }
    public string AnoPublicacao { get; set; }
    public decimal Preco { get; set; }
    public string FormaCompra { get; set; }
    public Autor Autor { get; set; }
    public Assunto Assunto { get; set; }
}
