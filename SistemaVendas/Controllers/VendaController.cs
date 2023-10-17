using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVendas.Dtos;
using System.Collections.Generic;

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        private readonly IVendaRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public VendaController(IVendaRepository repository, IClienteRepository clienteRepository, 
            IProdutoRepository produtoRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _contextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var vendasModel = await _repository.GetAllVendas();
                var vendasDto = _mapper.Map<List<VendaDto>>(vendasModel);
                return View(vendasDto);
            }
            catch 
            {
            }
            return View("Error");
        }

        #region Add
        public async Task<IActionResult> Add()
        {
            try
            {
                var clientesModel = await _clienteRepository.GetAllClientes();
                var produtosModel = await _produtoRepository.GetAllProdutosComEstoque();
                ViewBag.ListaClientes = _mapper.Map<List<ClienteDto>>(clientesModel);
                ViewBag.ListaProdutos = _mapper.Map<List<ProdutoDto>>(produtosModel);
                return View("Add");
            }
            catch
            {
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendaDto vendaDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var vendaModel = _mapper.Map<VendaModel>(vendaDto);
                    var vendaRegraNegocio = new VendaModel(_repository, _produtoRepository);
                    var idUserLogado = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("idUsuarioLogado"));
                    if (await vendaRegraNegocio.IncluiVendaEAlteraQuantidadeProduto(vendaModel, idUserLogado))
                        return RedirectToAction("Index");                    
                }
                else
                {
                    ViewBag.ListaClientes = _clienteRepository.GetAllClientes();
                    ViewBag.ListaProdutos = _produtoRepository.GetAllProdutosComEstoque();
                    return View("Add", vendaDto);
                }
            }
            catch 
            {
            }
            return View("Error");
        }
        #endregion Add

        #region Delete
        public async Task<IActionResult> Delete(int idVenda)
        {
            try
            {
                var vendaModel = await _repository.GetVendaPorId(idVenda);
                var vendaDto = _mapper.Map<VendaDto>(vendaModel);
                return View(vendaDto);
            }
            catch
            {
            }
            return View("Error");
        }
                
        public async Task<IActionResult> DeleteConfirm(int idVenda)
        {
            try
            {
                var vendaModel = await _repository.GetVendaPorId(idVenda);
                if(vendaModel != null)
                {
                    var vendaRegraNegocio = new VendaModel(_repository, _produtoRepository);
                    if (await vendaRegraNegocio.ExcluiVendaEAlteraQuantidadeProduto(vendaModel))
                        return RedirectToAction("Index");
                    else
                        return View("Error");                    
                }
            }
            catch
            {
            }
            return View("Error");
        }
        #endregion Delete
    }
}
