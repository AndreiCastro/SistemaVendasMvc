using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;

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
        public IActionResult Index()
        {
            try
            {
                var produtos = _repository.GetAllProdutos();
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
        public IActionResult Post(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Add(produto);
                    if (_repository.SaveChanges())
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
        public IActionResult Update(int idProduto)
        {
            try
            {
                var produto = _repository.GetProduto(idProduto);
                
                if (produto != null)
                {
                    produto.Quantidade_Estoque = Convert.ToInt32(produto.Quantidade_Estoque);
                    //Fiz essa conversão pra exibir o número inteiro na view Update
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
        public IActionResult Put(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var produtoDB = _repository.GetProduto(produto.Id);
                    if (produtoDB != null)
                    {
                        _repository.Update(produto);
                        if(_repository.SaveChanges())                        
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
        public IActionResult Delete(int idProduto)
        {
            try
            {
                var produto = _repository.GetProduto(idProduto);
                return View(produto);
            }
            catch
            {
                return View("Error");
            }
        }

        public IActionResult DeleteConfirm(int idProduto)
        {
            try
            {
                var produto = _repository.GetProduto(idProduto);
                if(produto != null)
                {
                    _repository.Delete(produto);
                    if(_repository.SaveChanges())
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
