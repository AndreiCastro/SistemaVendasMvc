using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SistemaVendas.Repository
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly Context _context;

        public VendedorRepository(Context context)
        {
                _context = context;
        }

        #region Selects
        public List<VendedorModel> GetAllVendedores()
        {
            return _context.Vendedores.AsNoTracking().OrderBy(x => x.Nome).ToList();
        }

        public VendedorModel GetVendedor(int idVendedor)
        {
            return _context.Vendedores.AsNoTracking().FirstOrDefault(x => x.Id == idVendedor);
        }
        #endregion Selects

        public void Add(VendedorModel vendedor)
        {
            _context.Add(vendedor);
        }

        public void Delete(VendedorModel vendedor)
        {
            _context.Remove(vendedor);
        }

        public void Update(VendedorModel vendedor)
        {
            _context.Update(vendedor);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }
    }
}
