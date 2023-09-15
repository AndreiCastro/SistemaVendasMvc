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

        public void Add(ClienteModel cliente)
        {
            _context.Add(cliente);
        }

        //public void Delete(ClienteModel cliente)
        //{
        //    throw new System.NotImplementedException();
        //}

        public List<ClienteModel> GetAllClientes()
        {
            return _context.Clientes.AsNoTracking().OrderBy(x => x.Nome).ToList();            
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        //public void Update(ClienteModel cliente)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
