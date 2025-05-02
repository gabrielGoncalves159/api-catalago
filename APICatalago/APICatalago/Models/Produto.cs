namespace APICatalago.Models;

public class Produto
{
    public int ProdutoId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public string? ImagemUrl { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    // Essa propriedade faz a reação nx1 com a tabela categoria
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
