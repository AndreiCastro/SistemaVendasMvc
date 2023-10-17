using SistemaVendas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Repositorys
{
    public interface IGraficoVendaRepository
    {
        Task<List<GraficoVendaModel>> GetSomaProdutoVendido();
    }
}
