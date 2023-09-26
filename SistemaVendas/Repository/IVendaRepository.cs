using SistemaVendas.Models;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Repository
{
    public interface IVendaRepository
    {
        List<VendaModel> GetAllVendas();

        VendaModel GetVenda(int idVenda);

        List<VendaModel> GetVendasPorPeriodo(DateTime dataDe, DateTime dataAte);

        void Add(VendaModel venda);

        void Delete(VendaModel venda);

        bool SaveChanges();
    }
}
