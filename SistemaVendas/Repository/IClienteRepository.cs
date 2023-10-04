using SistemaVendas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public interface IClienteRepository
    {
        Task<List<ClienteModel>> GetAllClientes();

        Task<ClienteModel> GetClientePorId(int id);

        void Add(ClienteModel cliente);

        void Delete(ClienteModel cliente);

        void Update(ClienteModel cliente);
        
        Task<bool> SaveChanges();
    }
}
