using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IGraficoVendaRepository _graficoVendaRepository;
        private readonly IMapper _mapper;

        public RelatorioController(IVendaRepository vendaRepository, IGraficoVendaRepository graficoVendaRepository, IMapper mapper)
        {
            _vendaRepository = vendaRepository;
            _graficoVendaRepository = graficoVendaRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        
        [HttpGet]
        public async Task<IActionResult> VendaPeriodo(DateTime dataDe, DateTime dataAte) 
        {
            try
            {
                var vendasModel = new List<VendaModel>();
                if (dataDe != new DateTime() || dataAte != new DateTime())
                    vendasModel = await _vendaRepository.GetVendaPorPeriodo(dataDe, dataAte);
                else
                    vendasModel = await _vendaRepository.GetAllVendas();

                var vendasDto = _mapper.Map<List<VendaDto>>(vendasModel);                
                return View(vendasDto);
            }
            catch 
            {
            }        
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Grafico()
        {
            var listaProdutosVendidos = await _graficoVendaRepository.GetSomaProdutoVendido();
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

            return View("Grafico");
        }
    }
}

