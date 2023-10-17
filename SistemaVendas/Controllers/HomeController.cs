using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class HomeController : Controller
    {
        #region Atributos
        private readonly ILoginRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        #endregion Atributos

        #region Construtor
        public HomeController(ILoginRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
        #endregion Construtor

        #region Menu
        public IActionResult Menu()
        {
            return View("Menu");
        }
        #endregion Menu

        #region Login
        [HttpGet]
        public IActionResult Login(int? id)
        {
            //Coloquei esse parametro como nulo, pois no _Layout.cshtml quando clicar no link sair limpa a sessão do user logado
            if (id != null)
            {
                if (id == 0)
                {
                    _contextAccessor.HttpContext.Session.SetString("idUsuarioLogado", string.Empty);
                    _contextAccessor.HttpContext.Session.SetString("nomeUsuarioLogado", string.Empty);
                }
            }
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(VendedorDto login)
        {
            try
            {
                var loginModel = _mapper.Map<VendedorModel>(login);
                var userLogin = await _repository.ValidarLogin(loginModel);
                if (userLogin != null)
                {
                    _contextAccessor.HttpContext.Session.SetString("idUsuarioLogado", userLogin.Id.ToString());
                    _contextAccessor.HttpContext.Session.SetString("nomeUsuarioLogado", userLogin.Nome);
                    return RedirectToAction("Menu");
                }
                else
                    TempData["ErrorLogin"] = "E-mail ou Senha inválidos!";
            }
            catch
            {
            }

            return View("Login", login);
        }
        #endregion Login

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
