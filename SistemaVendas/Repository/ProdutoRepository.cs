using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly Context _context;

        public ProdutoRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<ProdutoModel>> GetAllProdutos()
        {
            return await _context.Produtos.AsNoTracking().OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<ProdutoModel> GetProduto(int idProduto)
        {
            return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == idProduto);
        }

        public void Add(ProdutoModel produto)
        {
            _context.AddAsync(produto);
        }

        public void Delete(ProdutoModel produto)
        {
            _context.Remove(produto);
        }

        public void Update(ProdutoModel produto)
        {
            _context.Update(produto);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
