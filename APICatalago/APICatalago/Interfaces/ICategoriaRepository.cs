using APICatalago.Models;

namespace APICatalago.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias();
        IEnumerable<Categoria> GetCategoriasProdutos();
        Categoria GetCategoria(int id);
        Categoria Create(Categoria categoria);
        Categoria Update(Categoria categoria);
        Categoria Delete(int id);
    }
}
