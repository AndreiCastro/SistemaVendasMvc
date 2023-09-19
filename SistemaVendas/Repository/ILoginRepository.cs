using SistemaVendas.Models;

namespace SistemaVendas.Repository
{
    public interface ILoginRepository
    {
        VendedorModel ValidarLogin(VendedorModel login);
    }
}
