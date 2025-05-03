using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalago.Models;

[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        // Boa pratica sempre iniciar as propriedades Collection
        Produtos = new Collection<Produto>();
    }

    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    // Essa propriedade é responsavel pela ralação 1xN com a tabela Produtos
    public ICollection<Produto>? Produtos { get; set; }
}
