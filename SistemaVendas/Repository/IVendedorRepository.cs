using SistemaVendas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public interface IVendedorRepository
    {
        Task<List<VendedorModel>> GetAllVendedores();

        Task<VendedorModel> GetVendedorPorId(int idVendedor);

        void Add(VendedorModel vendedor);

        void Delete(VendedorModel vendedor);

        void Update(VendedorModel vendedor);

        Task<bool> SaveChanges();
    }
}
