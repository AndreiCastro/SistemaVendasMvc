using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class ClienteController : Controller
    {
        #region Atributos
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;
        #endregion Atributos

        #region Construtor
        public ClienteController(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion Construtor

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var clientesModel = await _repository.GetAllClientes();

                /* Carregando uma Model no Dto manualmente. Usando o automapper da pra fazer automaticamente, igual exemplo abaixo descomentado
                var listaClientesDto  = new List<ClienteDto>();
                foreach (var item in clientes)
                {
                    var clienteDto = new ClienteDto()
                    { 
                      Id = item.Id, Nome = item.Nome, 
                      CpfCnpj = item.CpfCnpj, Email = item.Email, Senha = item.Senha 
                    };
                    listaClientesDto.Add(clienteDto);
                } */

                var clientesDto = _mapper.Map<List<ClienteDto>>(clientesModel);
                return View(clientesDto);
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
        public async Task<IActionResult> Post(ClienteDto clienteDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var clienteModel = _mapper.Map<ClienteModel>(clienteDto);
                    _repository.Add(clienteModel);
                    if (await _repository.SaveChanges())                    
                        return RedirectToAction("Index");                    
                }
                return View("Add", clienteDto);
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
                var clienteModel = await _repository.GetClientePorId(idCliente);
                if (clienteModel != null)
                {
                    var clienteDto = _mapper.Map<ClienteDto>(clienteModel);
                    return View(clienteDto);
                }

                return View("Update");
            }
            catch 
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Put(ClienteDto clienteDto)
        {
            try 
            {                
                if (ModelState.IsValid)
                {
                    var clienteModel = _mapper.Map<ClienteModel>(clienteDto);
                    var clienteDB = await _repository.GetClientePorId(clienteModel.Id);
                    if (clienteDB != null)
                        _repository.Update(clienteModel);

                    if(await _repository.SaveChanges())
                        return RedirectToAction("Index");
                }                
                return View("Update", clienteDto);
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
                var clienteModel = await _repository.GetClientePorId(idCliente);
                var clienteDto = _mapper.Map<ClienteDto>(clienteModel);
                return View(clienteDto);
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
                var clienteModel = await _repository.GetClientePorId(idCliente);
                if (clienteModel != null)
                {
                    _repository.Delete(clienteModel);
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
