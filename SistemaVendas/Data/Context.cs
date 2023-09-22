using Microsoft.EntityFrameworkCore;
using SistemaVendas.Models;

namespace SistemaVendas.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<ClienteModel> Clientes { get; set; }

        public DbSet<VendedorModel> Vendedores { get; set; }

        public DbSet<ProdutoModel> Produtos { get; set; }

        public DbSet<VendaModel> Vendas { get; set; }
    }
}
