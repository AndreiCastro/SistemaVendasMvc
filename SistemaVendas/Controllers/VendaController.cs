using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using Microsoft.AspNetCore.Http;

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

        public IActionResult Index()
        {
            try
            {
                var vendas = _repository.GetAllVendas();
                return View(vendas);
            }
            catch 
            {
            }
            return View("Error");
        }

        #region Add
        public IActionResult Add()
        {
            try
            {
                var clientes = _repositoryCliente.GetAllClientes();
                var produtos = _repositoryProduto.GetAllProdutos();
                
                ViewBag.ListaClientes = clientes;
                ViewBag.ListaProdutos = produtos;

                return View();
            }
            catch
            {
            }
            return View("Error");
        }

        public IActionResult Post(VendaModel venda)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var produto = _repositoryProduto.GetProduto(venda.IdProduto);
                    if(produto != null)
                    {
                        produto.Quantidade_Estoque -= venda.Quantidade_Produto;
                        _repositoryProduto.Update(produto);
                        if(_repositoryProduto.SaveChanges())
                        {
                            venda.Data = DateTime.Now;
                            venda.Total = venda.Quantidade_Produto * produto.Preco_Unitario;
                            venda.IdVendedor = Convert.ToInt32(_contextAccessor.HttpContext.Session.GetString("idUsuarioLogado"));

                            _repository.Add(venda);
                            if (_repository.SaveChanges())                            
                                return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    var clientes = _repositoryCliente.GetAllClientes();
                    var produtos = _repositoryProduto.GetAllProdutos();

                    ViewBag.ListaClientes = clientes;
                    ViewBag.ListaProdutos = produtos;

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
        public IActionResult Delete(int idVenda)
        {
            try
            {
                var venda = _repository.GetVenda(idVenda);
                return View(venda);
            }
            catch
            {
            }
            return View("Error");
        }

        public IActionResult DeleteConfirm(int idVenda)
        {
            try
            {
                var venda = _repository.GetVenda(idVenda);
                var produto = _repositoryProduto.GetProduto(venda.IdProduto);
                if(produto != null)
                {
                    produto.Quantidade_Estoque += venda.Quantidade_Produto;
                    _repositoryProduto.Update(produto);
                    if(_repositoryProduto.SaveChanges())
                    {
                        _repository.Delete(venda);
                        if (_repository.SaveChanges())
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
