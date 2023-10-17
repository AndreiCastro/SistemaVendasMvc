using SistemaVendas.Models;
using System.Threading.Tasks;

namespace SistemaVendas.Repositorys
{
    public interface ILoginRepository
    {
        Task<VendedorModel> ValidarLogin(VendedorModel login);
    }
}
