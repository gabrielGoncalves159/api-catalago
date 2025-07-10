using APICatalago.Models;

namespace APICatalago.Dtos.Mappings;

public static class CategoriaDTOMappingExtensions
{
    /// <summary>
    /// Realiza a conversão da entidade Categoria para o DTO CategoriaDTO
    /// </summary>
    public static CategoriaDTO? ToCategoriaDTO(this Categoria categoria)
    {
        if(categoria == null) return null;

        return new CategoriaDTO()
        {
            CategoriaId = categoria.CategoriaId,
            Nome = categoria.Nome,
            ImagemUrl = categoria.ImagemUrl
        };
    }

    /// <summary>
    /// Realiza a conversão do DTO CategoriaDTO para a entidade Categoria
    /// </summary>
    public static Categoria? ToCategoria(this CategoriaDTO categoriaDTO)
    {
        if (categoriaDTO == null) return null;

        return new Categoria()
        {
            CategoriaId = categoriaDTO.CategoriaId,
            Nome = categoriaDTO.Nome,
            ImagemUrl= categoriaDTO.ImagemUrl
        };
    }

    /// <summary>
    /// Realiza a conversão de uma lista de entidades de Categoria para uma lista de CategoriaDTO
    /// </summary>
    public static List<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<Categoria> categorias)
    {
        var listaCategoriaDTO = new List<CategoriaDTO>();

        if (categorias is null || !categorias.Any()) return listaCategoriaDTO;

        foreach(var categoria in categorias)
        {
            listaCategoriaDTO.Add(new CategoriaDTO() 
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            });
        }

        return listaCategoriaDTO;
    }
}
