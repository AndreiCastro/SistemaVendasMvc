using Microsoft.EntityFrameworkCore;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
        public async Task<List<VendedorModel>> GetAllVendedores()
        {
            return await _context.Vendedores.AsNoTrackingWithIdentityResolution().OrderBy(x => x.Nome).ToListAsync();
        }

        public async Task<VendedorModel> GetVendedorPorId(int idVendedor)
        {
            return await _context.Vendedores.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == idVendedor);
        }
        #endregion Selects

        public void Add(VendedorModel vendedor)
        {
            _context.AddAsync(vendedor);
        }

        public void Delete(VendedorModel vendedor)
        {
            _context.Remove(vendedor);
        }

        public void Update(VendedorModel vendedor)
        {
            _context.Update(vendedor);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
