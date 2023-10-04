using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        private readonly IVendaRepository _repository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IHttpContextAccessor _contextAccessor;

        public VendaController(IVendaRepository repository, IClienteRepository clienteRepository, 
            IProdutoRepository produtoRepository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _contextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var vendas = await _repository.GetAllVendas();
                return View(vendas);
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
                ViewBag.ListaClientes = await _clienteRepository.GetAllClientes();
                ViewBag.ListaProdutos = await _produtoRepository.GetAllProdutosComEstoque();
                return View("Add");
            }
            catch
            {
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendaModel venda)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var vendaModel = new VendaModel(_repository, _produtoRepository);
                    var idUserLogado = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("idUsuarioLogado"));
                    if (await vendaModel.IncluiVendaEAlteraQuantidadeProduto(venda, idUserLogado))
                        return RedirectToAction("Index");                    
                }
                else
                {
                    ViewBag.ListaClientes = _clienteRepository.GetAllClientes();
                    ViewBag.ListaProdutos = _produtoRepository.GetAllProdutosComEstoque();
                    return View("Add", venda);
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
                var venda = await _repository.GetVendaPorId(idVenda);
                return View(venda);
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
                var venda = await _repository.GetVendaPorId(idVenda);
                if(venda != null)
                {
                    var vendaModel = new VendaModel(_repository, _produtoRepository);
                    if (await vendaModel.ExcluiVendaEAlteraQuantidadeProduto(venda))
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
