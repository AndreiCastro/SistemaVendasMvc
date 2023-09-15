using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Linq;

namespace SistemaVendas.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly Context _context;
        public LoginRepository(Context context)
        {
            _context = context;               
        }
        public LoginModel ValidarLogin(LoginModel login)
        {
            return _context.Vendedores.AsNoTracking().FirstOrDefault(l => l.Email == login.Email && l.Senha == login.Senha);
        }
    }
}
