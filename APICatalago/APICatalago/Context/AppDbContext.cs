using APICatalago.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Context
{
    public class AppDbContext : DbContext
    {
        //Recebe como parâmetro as opções de configurações que serão usadas para configurar o context do banco de dados
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Definição de mapeamento de entidades das classes
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
    }
}
