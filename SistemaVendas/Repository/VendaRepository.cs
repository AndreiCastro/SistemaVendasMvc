using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendas.Repository
{
    public class VendaRepository : IVendaRepository
    {
        private readonly Context _context;

        public VendaRepository(Context context)
        {
            _context = context;
        }

        public List<VendaModel> GetAllVendas()
        {
            var vendas = _context.Vendas.AsNoTracking();
            var clientes = _context.Clientes.AsNoTracking();
            var vendedores = _context.Vendedores.AsNoTracking();
            var produtos = _context.Produtos.AsNoTracking();

            var result = from venda in vendas
                         join cliente in clientes on venda.IdCliente equals cliente.Id
                         join vendedor in vendedores on venda.IdVendedor equals vendedor.Id
                         join produto in produtos on venda.IdProduto equals produto.Id
                         select new
                         {
                             Venda = venda,
                             Cliente = cliente,
                             Vendedor = vendedor,
                             Produto = produto,
                         };

            var lista = new List<VendaModel>();

            foreach (var v in result)
            {
                lista.Add(new VendaModel()
                {
                    Id = v.Venda.Id,
                    Data = v.Venda.Data,
                    Total = v.Venda.Total,
                    Quantidade_Produto = v.Venda.Quantidade_Produto,
                    IdVendedor = v.Venda.IdVendedor,
                    IdCliente = v.Venda.IdCliente,
                    IdProduto = v.Venda.IdProduto,
                    NomeVendedor = v.Vendedor.Nome,
                    NomeCliente = v.Cliente.Nome,
                    NomeProduto = v.Produto.Nome
                });
            }
            return lista;
        }

        public VendaModel GetVenda(int idVenda)
        {
            return _context.Vendas.AsNoTracking().FirstOrDefault(x => x.Id == idVenda);
        }


        public void Add(VendaModel venda)
        {
            _context.Add(venda);
        }

        public void Delete(VendaModel venda)
        {
            _context.Remove(venda);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
