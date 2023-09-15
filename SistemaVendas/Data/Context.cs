using Microsoft.EntityFrameworkCore;
using SistemaVendas.Models;

namespace SistemaVendas.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<LoginModel> Vendedores { get; set; }

        public DbSet<ClienteModel> Clientes { get; set; }
    }
}
