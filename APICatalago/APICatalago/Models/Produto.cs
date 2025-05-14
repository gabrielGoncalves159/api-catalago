using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalago.Models;

[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A descrição do produto é obrigatório")]
    [StringLength(300, ErrorMessage = "A descrição deve conter no máximo {1} caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O preço do produto é obrigatório")]
    [Column(TypeName="decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A URL da imagem é obrigatório")]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }

    // Essa propriedade faz a reação nx1 com a tabela categoria
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}
