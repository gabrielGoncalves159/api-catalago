using APICatalago.Context;
using APICatalago.Interfaces;
using APICatalago.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        // a palavra-chave 'base' é utilizada para acessar membros de classe base de dentro de uma classe derivada
        // nesse caso esta acessando Repository<Categoria>
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
