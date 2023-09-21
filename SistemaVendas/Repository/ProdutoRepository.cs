using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendas.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly Context _context;

        public ProdutoRepository(Context context)
        {
            _context = context;
        }

        public List<ProdutoModel> GetAllProdutos()
        {
            return _context.Produtos.AsNoTracking().OrderBy(x => x.Nome).ToList();
        }

        public ProdutoModel GetProduto(int idProduto)
        {
            return _context.Produtos.AsNoTracking().FirstOrDefault(x => x.Id == idProduto);
        }

        public void Add(ProdutoModel produto)
        {
            _context.Add(produto);
        }

        public void Delete(ProdutoModel produto)
        {
            _context.Remove(produto);
        }

        public void Update(ProdutoModel produto)
        {
            _context.Update(produto);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
