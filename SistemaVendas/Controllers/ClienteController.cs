using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SistemaVendas.Models;
using SistemaVendas.Repository;

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
        public IActionResult Index()
        {
            var clientes = _repository.GetAllClientes();
            return View(clientes);
        }

        #region Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.Add(cliente);
                    if (_repository.SaveChanges())
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View("Add", cliente);
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion Add

        #region Update
        public IActionResult Update(int id)
        {
            var cliente = new ClienteModel();
            try
            {
                cliente = _repository.GetCliente(id);
            }
            catch 
            {}

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Put(ClienteModel cliente)
        {
            try 
            {                
                if (ModelState.IsValid)
                {
                    var clienteRetornoDB = _repository.GetCliente(cliente.Id);
                    if (clienteRetornoDB != null)
                        _repository.Update(cliente);

                    if(_repository.SaveChanges())
                        return RedirectToAction("Index");
                }
                
                return View("Update", cliente);
            } 
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }           
            
        }
        #endregion Update

        #region Delete
        public IActionResult Delete(int id) 
        {
            var cliente = new ClienteModel();
            try
            {
                cliente = _repository.GetCliente(id);
            }
            catch
            {
            }

            return View(cliente);        
        }

        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                var cliente = _repository.GetCliente(id);
                if (cliente != null)
                {
                    _repository.Delete(cliente);
                    _repository.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }
        }
        #endregion Delete

    }
}
