using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using System.Collections.Generic;

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

        public IActionResult VendaPeriodo()
        {
            try
            {
                var vendas = _vendaRepository.GetAllVendas();
                ViewBag.ListaVendas = vendas;
                return View();
            }
            catch 
            {
            }

            return View("Error");
        }

        [HttpGet]
        public IActionResult SelectVendaPeriodo(RelatorioModel relatorio) 
        {
            try
            {
                var vendas = new List<VendaModel>();
                
                if (relatorio.DataDe == new DateTime() || relatorio.DataAte == new DateTime())                
                    vendas = _vendaRepository.GetAllVendas();                
                else
                    vendas = _vendaRepository.GetVendasPorPeriodo(relatorio.DataDe, relatorio.DataAte);

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
