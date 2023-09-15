using SistemaVendas.Models;
using System.Collections.Generic;

namespace SistemaVendas.Repository
{
    public interface IClienteRepository
    {
        List<ClienteModel> GetAllClientes();

        void Add(ClienteModel cliente);

        //void Update(ClienteModel cliente);

        //void Delete(ClienteModel cliente);

        bool SaveChanges();
    }
}
