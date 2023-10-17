using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Repositorys
{
    public class VendaRepository : IVendaRepository
    {
        private readonly Context _context;

        public VendaRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<VendaModel>> GetAllVendas()
        {
            /* Exemplo de LINQ com JOIN
            var vendas = _context.Vendas.AsNoTrackingWithIdentityResolution();
            var clientes = _context.Clientes.AsNoTrackingWithIdentityResolution();
            var vendedores = _context.Vendedores.AsNoTrackingWithIdentityResolution();
            var produtos = _context.Produtos.AsNoTrackingWithIdentityResolution();

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
                    QuantidadeProduto = v.Venda.QuantidadeProduto,
                    IdVendedor = v.Venda.IdVendedor,
                    IdCliente = v.Venda.IdCliente,
                    IdProduto = v.Venda.IdProduto,
                    //NomeVendedor = v.Vendedor.Nome,
                    //NomeCliente = v.Cliente.Nome,
                    //NomeProduto = v.Produto.Nome
                });
            }
            return lista.OrderBy(x => x.Data).ToList();
            */

            return await _context.Vendas.AsNoTrackingWithIdentityResolution()
                .Include(c => c.Cliente)
                .Include(p => p.Produto)
                .Include(v => v.Vendedor)
                .ToListAsync();
        }

        public async Task<List<VendaModel>> GetVendaPorPeriodo(DateTime dataDe, DateTime dataAte)
        {
            /* Exemplo de LINQ com JOIN
            var vendas = _context.Vendas.AsNoTrackingWithIdentityResolution();
            var vendedores = _context.Vendedores.AsNoTrackingWithIdentityResolution();
            var clientes = _context.Clientes.AsNoTrackingWithIdentityResolution();
            var produtos = _context.Produtos.AsNoTrackingWithIdentityResolution();

            var result = from venda in vendas
                         join cliente in clientes on venda.IdCliente equals cliente.Id
                         join vendedor in vendedores on venda.IdVendedor equals vendedor.Id
                         join produto in produtos on venda.IdProduto equals produto.Id
                         where venda.Data >= dataDe && venda.Data <= dataAte
                         select new
                         {
                             Venda = venda,
                             Vendedor = vendedor,
                             Cliente = cliente,
                             Produto = produto
                         };

            var lista = new List<VendaModel>();

            foreach (var venda in result)
            {
                lista.Add(new VendaModel()
                {
                    Id = venda.Venda.Id,
                    Data = venda.Venda.Data,
                    Total = venda.Venda.Total,
                    QuantidadeProduto = venda.Venda.QuantidadeProduto,
                    //NomeVendedor = venda.Vendedor.Nome,
                    //NomeCliente = venda.Cliente.Nome,
                    //NomeProduto = venda.Produto.Nome
                });
            }

            return lista;
            */

            return await _context.Vendas.AsNoTrackingWithIdentityResolution()
                .Include(c => c.Cliente)
                .Include(p => p.Produto)
                .Include(v => v.Vendedor)
                .Where(x => x.Data >= dataDe && x.Data <= dataAte)
                .ToListAsync();
        }        

        public async Task<VendaModel> GetVendaPorId(int idVenda)
        {
            return await _context.Vendas.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == idVenda);
        }

        public void Add(VendaModel venda)
        {
            _context.AddAsync(venda);
        }

        public void Delete(VendaModel venda)
        {
            _context.Remove(venda);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
