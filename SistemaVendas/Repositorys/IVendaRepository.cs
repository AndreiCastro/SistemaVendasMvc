using SistemaVendas.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Repositorys
{
    public interface IVendaRepository
    {
        Task<List<VendaModel>> GetAllVendas();

        Task<VendaModel> GetVendaPorId(int idVenda);        

        Task<List<VendaModel>> GetVendaPorPeriodo(DateTime dataDe, DateTime dataAte);

        void Add(VendaModel venda);

        void Delete(VendaModel venda);

        Task<bool> SaveChanges();
    }
}
