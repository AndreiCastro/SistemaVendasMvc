using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVendas.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context _context;

        public ClienteRepository(Context context)
        {
            _context = context;
        }

        public List<ClienteModel> GetAllClientes()
        {
            return _context.Clientes.AsNoTracking().OrderBy(x => x.Nome).ToList();
        }

        public ClienteModel GetCliente(int id)
        {
            return _context.Clientes.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Add(ClienteModel cliente)
        {
            _context.Add(cliente);
        }

        public void Delete(ClienteModel cliente)
        {
            _context.Remove(cliente);
        }

        public void Update(ClienteModel cliente)
        {
            _context.Update(cliente);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
