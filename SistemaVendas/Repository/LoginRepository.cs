using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Linq;

namespace SistemaVendas.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly Context context;
        public LoginRepository(Context _context)
        {
            this.context = _context;               
        }
        public LoginModel ValidarLogin(LoginModel login)
        {
            var userLogin = context.Vendedores.AsNoTracking().FirstOrDefault(l => l.Email == login.Email && l.Senha == login.Senha);

            return userLogin;
        }
    }
}
