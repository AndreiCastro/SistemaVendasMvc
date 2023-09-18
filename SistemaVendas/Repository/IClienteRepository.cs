using SistemaVendas.Models;
using System.Collections.Generic;

namespace SistemaVendas.Repository
{
    public interface IClienteRepository
    {
        List<ClienteModel> GetAllClientes();

        ClienteModel GetCliente(int id);

        void Add(ClienteModel cliente);

        void Delete(ClienteModel cliente);

        void Update(ClienteModel cliente);
        
        bool SaveChanges();
    }
}
