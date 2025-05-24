using APICatalago.Context;
using APICatalago.Interfaces;
using APICatalago.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Produto> GetProdutos()
        {
            return _context.Produtos;
        }

        public Produto GetProduto(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            return produto;
        }

        public Produto Create(Produto produto)
        {
            if (produto is null) 
            {
                throw new ArgumentNullException(nameof(produto));
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return produto;
        }

        public bool Update(Produto produto)
        {
            if (produto is null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            if (_context.Produtos.Any( c => c.ProdutoId == produto.ProdutoId))
            {
                //Essa abordagem é para quando a entidade já esta rastreada
                _context.Produtos.Update(produto);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is not null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
