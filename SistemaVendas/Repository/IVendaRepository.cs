using SistemaVendas.Models;
using System.Collections.Generic;

namespace SistemaVendas.Repository
{
    public interface IVendaRepository
    {
        List<VendaModel> GetAllVendas();

        VendaModel GetVenda(int idVenda);

        void Add(VendaModel venda);

        void Delete(VendaModel venda);

        bool SaveChanges();
    }
}
