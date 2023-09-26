using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly Context _context;
        public LoginRepository(Context context)
        {
            _context = context;               
        }
        public async Task<VendedorModel> ValidarLogin(VendedorModel login)
        {
            return await _context.Vendedores.AsNoTracking().FirstOrDefaultAsync(l => l.Email == login.Email && l.Senha == login.Senha);
        }
    }
}
