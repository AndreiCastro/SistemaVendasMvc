using SistemaVendas.Models;
using System.Collections.Generic;

namespace SistemaVendas.Repository
{
    public interface IVendedorRepository
    {
        List<VendedorModel> GetAllVendedores();

        VendedorModel GetVendedor(int idVendedor);

        void Add(VendedorModel vendedor);

        void Delete(VendedorModel vendedor);

        void Update(VendedorModel vendedor);

        bool SaveChanges();
    }
}
