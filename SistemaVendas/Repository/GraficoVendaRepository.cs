using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaVendas.Data;
using SistemaVendas.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public class GraficoVendaRepository : IGraficoVendaRepository
    {
        private readonly Context _context;

        public GraficoVendaRepository(Context context)
        {
            _context = context;
        }
        public async Task<List<GraficoVendaModel>> GetSomaProdutoVendido()
        {
            var vendas = _context.Vendas.AsNoTrackingWithIdentityResolution();
            var produtos = _context.Produtos.AsNoTrackingWithIdentityResolution();

            var listaAgrupamento = from venda in vendas
                                   join produto in produtos on venda.IdProduto equals produto.Id
                                   group venda by produto.Nome into grupo
                                   orderby grupo.Key
                                   select new
                                   {
                                       Nome = grupo.Key,
                                       Quantidade = grupo.Sum(x => x.Quantidade_Produto)
                                   };

            var lista = new List<GraficoVendaModel>();
            foreach (var item in listaAgrupamento)
            {
                lista.Add(new GraficoVendaModel()
                {
                    QuantidadeVendida = item.Quantidade,
                    DescricaoProduto = item.Nome
                });                
            }
            return await Task.Run(() => lista);  //Pra usar a lista como assíncrona
        }
    }
}
