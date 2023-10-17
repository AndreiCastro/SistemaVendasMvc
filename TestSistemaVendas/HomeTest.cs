using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SistemaVendas.Controllers;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System.Threading.Tasks;

namespace TestSistemaVendas
{
    [TestFixture]    
    public class HomeTest
    {
        private HomeController _controller;
        private Mock<ILoginRepository> _repository;
        private Mock<IMapper> _mapper;
        private Mock<IHttpContextAccessor> _contextAcessor;
        VendedorModel vendedorModel;
        VendedorDto vendedorDto;
        
        [SetUp]
        [Category("SetUp")]
        public void SetUp()
        {
            _repository = new Mock<ILoginRepository>();
            _mapper = new Mock<IMapper>();
            _contextAcessor = new Mock<IHttpContextAccessor>();
            _controller = new HomeController(_repository.Object, _mapper.Object, _contextAcessor.Object);
            

            vendedorModel = VendedorTest.PopulaVendedorModel();
            vendedorDto = VendedorTest.PopulaVendedorDto();

            //Arrange
            _repository.Setup(x => x.ValidarLogin(vendedorModel)).ReturnsAsync(vendedorModel);
            _mapper.Setup(x => x.Map<VendedorDto>(vendedorModel)).Returns(vendedorDto);
            _mapper.Setup(x => x.Map<VendedorModel>(vendedorDto)).Returns(vendedorModel);
            _contextAcessor.Setup(x => x.HttpContext.Session.Equals(1));
        }

        [Test]
        [Category("Menu")]
        public void Menu()
        {
            //Arrange já declarado SetUp
            //Act
            var result = _controller.Menu() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewName);
            Assert.That(result.ViewName, Is.EqualTo("Menu"));
        }

        [Test]
        [Category("Login")]
        public void LoginPorId()
        {
            //Arrange já declarado SetUp
            //Act
            var result = _controller.Login(vendedorDto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewName);
            Assert.That(result.ViewName, Is.EqualTo("Login"));
        }

        [Test]
        [Category("Login")]
        public async Task Login()
        {
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.Login(vendedorDto) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Menu"));
        }
    }
}
