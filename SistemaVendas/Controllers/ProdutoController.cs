using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SistemaVendas.Enums.Enumeradores;

namespace SistemaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var produtosModel = await _repository.GetAllProdutos();
                var produtosDto = _mapper.Map<List<ProdutoDto>>(produtosModel);
                return View(produtosDto);
            }
            catch
            {
                return View("Error");
            }
        }

        #region Add
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoDto produtoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var produtoModel = _mapper.Map<ProdutoModel>(produtoDto);                    
                    produtoModel.UnidadeMedida = ((UnidadeMedida)Convert.ToInt32(produtoModel.UnidadeMedida)).ToString();
                    _repository.Add(produtoModel);
                    if (await _repository.SaveChanges())
                        return RedirectToAction("Index");
                }
                return View("Add", produtoDto);
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
                var produtoModel = await _repository.GetProdutoPorId(idProduto);                
                if (produtoModel != null)
                {
                    var produtoDto = _mapper.Map<ProdutoDto>(produtoModel);
                    ViewBag.UnidadeMedida = produtoDto.UnidadeMedida;        
                    return View(produtoDto);
                }
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Put(ProdutoDto produtoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var produtoModel = _mapper.Map<ProdutoModel>(produtoDto);
                    produtoModel.UnidadeMedida = ((UnidadeMedida)Convert.ToInt32(produtoModel.UnidadeMedida)).ToString();
                    var produtoDB = await _repository.GetProdutoPorId(produtoModel.Id);
                    if (produtoDB != null)
                    {
                        _repository.Update(produtoModel);
                        if(await _repository.SaveChanges())                        
                            return RedirectToAction("Index");                        
                    }
                }
                return View("Update", produtoDto);
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
                var produtoModel = await _repository.GetProdutoPorId(idProduto);
                var produtoDto = _mapper.Map<ProdutoDto>(produtoModel);
                return View(produtoDto);
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
                var produtoModel = await _repository.GetProdutoPorId(idProduto);
                if(produtoModel != null)
                {
                    _repository.Delete(produtoModel);
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
