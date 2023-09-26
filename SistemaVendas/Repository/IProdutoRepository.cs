﻿using SistemaVendas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public interface IProdutoRepository
    {
        Task<List<ProdutoModel>> GetAllProdutos();
        Task<ProdutoModel> GetProduto(int idProduto);

        void Add(ProdutoModel produto);
        void Delete(ProdutoModel produto);
        void Update(ProdutoModel produto);
        Task<bool> SaveChanges();
    }
}
