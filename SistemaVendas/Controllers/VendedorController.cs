using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;
using SistemaVendas.Repository;

namespace SistemaVendas.Controllers
{
    public class VendedorController : Controller
    {
        private readonly IVendedorRepository _repository;

        public VendedorController(IVendedorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var vendedores = _repository.GetAllVendedores();
                return View(vendedores);
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
        public IActionResult Post(VendedorModel vendedor)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _repository.Add(vendedor);
                    if(_repository.SaveChanges())
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
        public IActionResult Update(int idVendedor) //Este parametro tem que ter o mesmo nome que colocou asp-route-"idVendedor" na Index 
        {
            try
            {
                var vendedor = _repository.GetVendedor(idVendedor);
                if(vendedor != null)
                    return View(vendedor);
                
                return View();
            }
            catch 
            {
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Put(VendedorModel vendedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vendedorDB = _repository.GetVendedor(vendedor.Id);
                    if (vendedorDB != null)
                    {
                        _repository.Update(vendedor);
                        if (_repository.SaveChanges())
                            return RedirectToAction("Index");
                    }
                }
                return View("Update", vendedor);
            }
            catch 
            {
                return View("Error");
            }
        }
        #endregion Update

        #region Delete
        public IActionResult Delete(int idVendedor)
        {
            try
            {
                var vendedor = _repository.GetVendedor(idVendedor);
                return View(vendedor);
            }
            catch 
            {
                return View("Error");
            }
        }

        public IActionResult DeleteConfirm(int idVendedor)
        {
            try
            {
                var vendedor = _repository.GetVendedor(idVendedor);
                if (vendedor != null)
                {
                    _repository.Delete(vendedor);
                    if (_repository.SaveChanges())
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
