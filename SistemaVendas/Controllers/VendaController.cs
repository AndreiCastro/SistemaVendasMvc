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
        private readonly IClienteRepository _repositoryCliente;
        private readonly IProdutoRepository _repositoryProduto;
        private readonly IHttpContextAccessor _contextAccessor;

        public VendaController(IVendaRepository repository, IClienteRepository repositoryCliente, 
            IProdutoRepository repositoryProduto, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _repositoryCliente = repositoryCliente;
            _repositoryProduto = repositoryProduto;
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
                ViewBag.ListaClientes = await _repositoryCliente.GetAllClientes();
                ViewBag.ListaProdutos = await _repositoryProduto.GetAllProdutos();
                return View();
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
                    var produto = await _repositoryProduto.GetProduto(venda.IdProduto);
                    if(produto != null)
                    {
                        produto.QuantidadeEstoque -= venda.Quantidade_Produto;
                        _repositoryProduto.Update(produto);
                        if( await _repositoryProduto.SaveChanges())
                        {
                            venda.Data = DateTime.Now;
                            venda.Total = venda.Quantidade_Produto * produto.PrecoUnitario;
                            venda.IdVendedor = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("idUsuarioLogado"));

                            _repository.Add(venda);
                            if (await _repository.SaveChanges())                            
                                return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ViewBag.ListaClientes = _repositoryCliente.GetAllClientes();
                    ViewBag.ListaProdutos = _repositoryProduto.GetAllProdutos();
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
                var venda = await _repository.GetVenda(idVenda);
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
                var venda = await _repository.GetVenda(idVenda);
                var produto = await _repositoryProduto.GetProduto(venda.IdProduto);
                if(produto != null)
                {
                    produto.QuantidadeEstoque += venda.Quantidade_Produto;
                    _repositoryProduto.Update(produto);
                    if(await _repositoryProduto.SaveChanges())
                    {
                        _repository.Delete(venda);
                        if (await _repository.SaveChanges())
                            return RedirectToAction("Index");
                    }
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
