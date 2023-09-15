using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    }
}
