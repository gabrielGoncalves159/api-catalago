using APICatalago.Dtos;
using APICatalago.Models;
using AutoMapper;

namespace APICatalago.DTOs.Domains;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
    }

}
