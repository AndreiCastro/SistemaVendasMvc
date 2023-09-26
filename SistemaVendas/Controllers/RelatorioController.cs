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

        public async Task<IActionResult> VendaPeriodo()
        {
            try
            {
                var vendas = await _vendaRepository.GetAllVendas();
                ViewBag.ListaVendas = vendas;
                return View();
            }
            catch 
            {
            }

            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> VendaPeriodo(RelatorioModel relatorio) 
        {
            try
            {
                var vendas = new List<VendaModel>();
                
                if (relatorio.DataDe == new DateTime() || relatorio.DataAte == new DateTime())                
                    vendas = await _vendaRepository.GetAllVendas();                
                else
                    vendas = await _vendaRepository.GetVendasPorPeriodo(relatorio.DataDe, relatorio.DataAte);

                ViewBag.ListaVendas = vendas;
                return View("VendaPeriodo");
            }
            catch 
            {
            }
        
            return View("Error");
        }
    }
}
