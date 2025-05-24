using APICatalago.Context;
using APICatalago.Interfaces;
using APICatalago.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Categoria GetCategoria(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            //O AsNoTracking melhora o desempenho da consulta, eliminando o cache das entidades do contexto
            //Deve ser usado paneas em funções de leitura aonde eu não sei o estado atual dos objetos.

            //TODO: Nesse trecho de código o ideal é realizar uma filtragem, nunca retornar todos os registros de uma vez, isso resulta
            // em problema de desenpanho, nesse capo pode-se utilizar o método 'Where()' para realizar um filtro
            return _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
        }
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            //O AsNoTracking melhora o desempenho da consulta, eliminando o cache das entidades do contexto
            //Deve ser usado paneas em funções de leitura aonde eu não sei o estado atual dos objetos.
            return _context.Categorias.AsNoTracking().ToList();
        }

        public Categoria Create(Categoria categoria)
        {
            if(categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }

        public Categoria Update(Categoria categoria)
        {
            if(categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Categorias.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return categoria;
        }

        public Categoria Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if(categoria is null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return categoria;
        }
    }
}
