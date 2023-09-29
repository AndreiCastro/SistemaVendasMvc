using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public interface IVendaRepository
    {
        Task<List<VendaModel>> GetAllVendas();

        Task<VendaModel> GetVenda(int idVenda);        

        Task<List<VendaModel>> GetVendasPorPeriodo(DateTime dataDe, DateTime dataAte);

        void Add(VendaModel venda);

        void Delete(VendaModel venda);

        Task<bool> SaveChanges();
    }
}
