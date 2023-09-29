using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IVendaRepository _vendaRepository;

        public RelatorioController(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> VendaPeriodo(DateTime dataDe, DateTime dataAte) 
        {
            try
            {
                var vendas = new List<VendaModel>();
                if (dataDe != new DateTime() || dataAte != new DateTime())
                    vendas = await _vendaRepository.GetVendasPorPeriodo(dataDe, dataAte);
                else
                    vendas = await _vendaRepository.GetAllVendas();                                    

                ViewBag.ListaVendas = vendas;
                return View("VendaPeriodo");
            }
            catch 
            {
            }        
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Grafico()
        {
            var listaProdutosVendidos = await _vendaRepository.GetSomaProdutoVendido();
            string valores = null, labels = null, cores = null;
            var random = new Random();

            foreach (var item in listaProdutosVendidos)
            {
                valores += $"{item.QuantidadeVendida},";
                labels += $"'{item.DescricaoProduto}',";
                //random nas cores
                cores += $"'{string.Format("#{0:X6}", random.Next(0x1000000))}',";
            }
            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }
    }
}

