using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context _context;

        public ClienteRepository(Context context)
        {
            _context = context;
        }

        #region Selects
        public async Task<List<ClienteModel>> GetAllClientes()
        {
            return await _context.Clientes.AsNoTrackingWithIdentityResolution().OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<ClienteModel> GetClientePorId(int id)
        {
            return await _context.Clientes.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id);
        }
        #endregion Selects

        public void Add(ClienteModel cliente)
        {
            _context.AddAsync(cliente);
        }

        public void Delete(ClienteModel cliente)
        {
            _context.Remove(cliente);
        }

        public void Update(ClienteModel cliente)
        {
            _context.Update(cliente);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
