using System.Collections.ObjectModel;

namespace APICatalago.Models;

public class Categoria
{
    public Categoria()
    {
        // Boa pratica sempre iniciar as propriedades Collection
        Produtos = new Collection<Produto>();
    }
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }

    // Essa propriedade é responsavel pela ralação 1xN com a tabela Produtos
    public ICollection<Produto>? Produtos { get; set; }
}
