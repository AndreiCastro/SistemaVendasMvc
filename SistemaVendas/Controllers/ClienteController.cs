using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class ClienteController : Controller
    {
        #region Atributos
        private readonly IClienteRepository _repository;
        #endregion Atributos

        #region Construtor
        public ClienteController(IClienteRepository repository)
        {
            _repository = repository;
        }
        #endregion Construtor

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var clientes = await _repository.GetAllClientes();
                return View(clientes);
            }
            catch
            {
                return View("Error");                
            }            
        }

        #region Add
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Add(cliente);
                    if (await _repository.SaveChanges())                    
                        return RedirectToAction("Index");                    
                }
                return View("Add", cliente);
            }
            catch
            {
                return View("Error");
            }
        }
        #endregion Add

        #region Update
        public async Task<IActionResult> Update(int idCliente) //Este parametro tem que ter o mesmo nome que colocou asp-route-"idCliente" na Index 
        {            
            try
            {
                var cliente = await _repository.GetClientePorId(idCliente);
                if(cliente != null)
                    return View(cliente);

                return View("Update");
            }
            catch 
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Put(ClienteModel cliente)
        {
            try 
            {                
                if (ModelState.IsValid)
                {
                    var clienteRetornoDB = await _repository.GetClientePorId(cliente.Id);
                    if (clienteRetornoDB != null)
                        _repository.Update(cliente);

                    if(await _repository.SaveChanges())
                        return RedirectToAction("Index");
                }                
                return View("Update", cliente);
            } 
            catch
            {
                return View("Error");
            }            
        }
        #endregion Update

        #region Delete
        public async Task<IActionResult> Delete(int idCliente) 
        {
            try
            {
                var cliente = await _repository.GetClientePorId(idCliente);
                return View(cliente);
            }
            catch
            {
                return View("Error");
            }      
        }

        public async Task<IActionResult> DeleteConfirm(int idCliente)
        {
            try
            {
                var cliente = await _repository.GetClientePorId(idCliente);
                if (cliente != null)
                {
                    _repository.Delete(cliente);
                    if(await _repository.SaveChanges());
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
