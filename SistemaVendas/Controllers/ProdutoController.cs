using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _repository;

        public ProdutoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var produtos = await _repository.GetAllProdutos();
                return View(produtos);
            }
            catch
            {
                return View("Error");
            }
        }

        #region Add
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Add(produto);
                    if (await _repository.SaveChanges())
                        return RedirectToAction("Index");
                }
                return View("Add", produto);
            }
            catch
            {
                return View("Error");
            }
        }
        #endregion Add

        #region Update
        public async Task<IActionResult> Update(int idProduto)
        {
            try
            {
                var produto = await _repository.GetProduto(idProduto);
                
                if (produto != null)
                {
                    ViewBag.UnidadeMedida = produto.UnidadeMedida;                 
                    return View(produto);
                }
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Put(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var produtoDB = await _repository.GetProduto(produto.Id);
                    if (produtoDB != null)
                    {
                        _repository.Update(produto);
                        if(await _repository.SaveChanges())                        
                            return RedirectToAction("Index");                        
                    }
                }
                return View("Update", produto);
            }
            catch 
            {
                return View("Error");
            }
        }
        #endregion Update

        #region Delete
        public async Task<IActionResult> Delete(int idProduto)
        {
            try
            {
                var produto = await _repository.GetProduto(idProduto);
                return View(produto);
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> DeleteConfirm(int idProduto)
        {
            try
            {
                var produto = await _repository.GetProduto(idProduto);
                if(produto != null)
                {
                    _repository.Delete(produto);
                    if(await _repository.SaveChanges())
                        return RedirectToAction("Index");
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
