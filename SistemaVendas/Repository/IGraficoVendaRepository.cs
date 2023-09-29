using SistemaVendas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public interface IGraficoVendaRepository
    {
        Task<List<GraficoVendaModel>> GetSomaProdutoVendido();
    }
}
