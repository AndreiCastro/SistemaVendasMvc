using SistemaVendas.Models;
using System.Collections.Generic;

namespace SistemaVendas.Repository
{
    public interface IProdutoRepository
    {
        List<ProdutoModel> GetAllProdutos();
        ProdutoModel GetProduto(int idProduto);

        void Add(ProdutoModel produto);
        void Delete(ProdutoModel produto);
        void Update(ProdutoModel produto);
        bool SaveChanges();
    }
}
