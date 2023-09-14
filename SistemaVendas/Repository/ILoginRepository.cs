using SistemaVendas.Models;

namespace SistemaVendas.Repository
{
    public interface ILoginRepository
    {
        LoginModel ValidarLogin(LoginModel login);
    }
}
