using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class VendedorController : Controller
    {
        private readonly IVendedorRepository _repository;
        private readonly IMapper _mapper;

        public VendedorController(IVendedorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var vendedoresModel = await _repository.GetAllVendedores();
                var vendedoresDto = _mapper.Map<List<VendedorDto>>(vendedoresModel);
                return View(vendedoresDto);
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
        public async Task<IActionResult> Post(VendedorDto vendedor)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var vendedorModel = _mapper.Map<VendedorModel>(vendedor);
                    _repository.Add(vendedorModel);
                    if(await _repository.SaveChanges())
                        return RedirectToAction("Index");
                }
                return View("Add", vendedor);
            }
            catch
            {
                return View("Error");
            }
        }
        #endregion Add

        #region Update
        public async Task<IActionResult> Update(int idVendedor) //Este parametro tem que ter o mesmo nome que colocou asp-route-"idVendedor" na Index 
        {
            try
            {
                var vendedorModel = await _repository.GetVendedorPorId(idVendedor);                
                if (vendedorModel != null)
                {
                    var vendedorDto = _mapper.Map<VendedorDto>(vendedorModel);
                    return View(vendedorDto);
                }
                
                return View();
            }
            catch 
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Put(VendedorDto vendedorDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vendedorModel = _mapper.Map<VendedorModel>(vendedorDto);
                    var vendedorDB = await _repository.GetVendedorPorId(vendedorModel.Id);                    
                    if (vendedorDB != null)
                    {
                        _repository.Update(vendedorModel);
                        if (await _repository.SaveChanges())
                            return RedirectToAction("Index");
                    }
                }
                return View("Update", vendedorDto);
            }
            catch 
            {
                return View("Error");
            }
        }
        #endregion Update

        #region Delete
        public async Task<IActionResult> Delete(int idVendedor)
        {
            try
            {
                var vendedorModel = await _repository.GetVendedorPorId(idVendedor);
                var vendedorDto = _mapper.Map<VendedorDto>(vendedorModel);
                return View(vendedorDto);
            }
            catch 
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> DeleteConfirm(int idVendedor)
        {
            try
            {
                var vendedorModel = await _repository.GetVendedorPorId(idVendedor);
                if (vendedorModel != null)
                {
                    _repository.Delete(vendedorModel);
                    if (await _repository.SaveChanges())
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
