using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginRepository repository;
        public HomeController(ILoginRepository _repository)
        {
            this.repository = _repository;
        }

        public IActionResult Menu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(int? id)
        {
            //Coloquei esse parametro como nulo, pois no _Layout.cshtml quando clicar no link sair limpa a sessão do user logado
            if(id != null)
            {
                if(id == 0)
                {
                    HttpContext.Session.SetString("idUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("nomeUsuarioLogado", string.Empty);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogin = repository.ValidarLogin(login);
                    if (userLogin != null)
                    {
                        HttpContext.Session.SetString("idUsuarioLogado", userLogin.Id.ToString());
                        HttpContext.Session.SetString("nomeUsuarioLogado", userLogin.Nome);
                        return RedirectToAction("Menu");
                    }
                    else
                        TempData["ErrorLogin"] = "E-mail ou Senha inválidos!";
                }

            }
            catch (Exception)
            {

                throw;
            }

            return View("Login", login);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
