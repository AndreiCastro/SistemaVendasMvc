using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SistemaVendas.Controllers;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestSistemaVendas
{
    [TestFixture]
    public class VendedorTest
    {
        private VendedorController _controller;
        private Mock<IVendedorRepository> _repository;
        private Mock<IMapper> _mapper;
        private Mock<IHttpContextAccessor> _contextAcessor; 

        List<VendedorModel> vendedoresModel = new List<VendedorModel>();
        VendedorModel vendedorModel = new VendedorModel();
        List<VendedorDto> vendedoresDto = new List<VendedorDto>();
        VendedorDto vendedorDto = new VendedorDto();

        [SetUp]
        [Category("SetUp")]
        public void SetUp()
        {
            _repository = new Mock<IVendedorRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new VendedorController(_repository.Object, _mapper.Object);
            _contextAcessor = new Mock<IHttpContextAccessor>();
            
            vendedoresModel = PopulaAllVendedoresModel();
            vendedorModel = PopulaVendedorModel();
            vendedoresDto = PopulaAllVendedoresDto();
            vendedorDto = PopulaVendedorDto();

            //Arrange
            _repository.Setup(x => x.GetAllVendedores()).ReturnsAsync(vendedoresModel);
            _mapper.Setup(x => x.Map<List<VendedorDto>>(vendedoresModel)).Returns(vendedoresDto);
            _repository.Setup(x => x.GetVendedorPorId(vendedorDto.Id)).ReturnsAsync(vendedorModel);
            _mapper.Setup(x => x.Map<VendedorDto>(vendedorModel)).Returns(vendedorDto);
            _mapper.Setup(x => x.Map<VendedorModel>(vendedorDto)).Returns(vendedorModel);
            _repository.Setup(x => x.SaveChanges()).ReturnsAsync(true);

        }

        [Test]
        [Category("Index")]
        public async Task Index()
        {
            //Arrange já declarado SetUp
            //Act 
            var result = await _controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendedoresDto));

            var vendedoresModelNotNull = result.Model as List<VendedorDto>;
            Assert.That(vendedoresModelNotNull.Count, Is.EqualTo(vendedoresModel.Count));

        }

        [Test]
        [Category("Add")]
        public void Add()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = _controller.Add() as ViewResult;

            //Assert
            Assert.IsNotNull(result.ViewName);
            Assert.That(result.ViewName, Is.EqualTo("Add"));
        }

        [Test]
        [Category("Add")]
        public async Task Post()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Post(vendedorDto) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Update")]
        public async Task Update()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Update(vendedorDto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendedorDto));

            var vendedorNotNull = result.Model as VendedorDto;
            Assert.That(vendedorNotNull, Is.EqualTo(vendedorDto));
        }

        [Test]
        [Category("Update")]
        public async Task Put()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Put(vendedorDto) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Delete")]
        public async Task Delete()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Delete(vendedorDto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendedorDto));

            var vendedorNotNull = result.Model as VendedorDto;
            Assert.That(vendedorNotNull, Is.EqualTo(vendedorDto));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.DeleteConfirm(vendedorDto.Id) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        #region PopulaClasses
        private List<VendedorModel> PopulaAllVendedoresModel()
        {
            for (int i = 1; i < 3; i++)
            {
                vendedorModel = new VendedorModel()
                {
                    Id = i,
                    Nome = $"Test mock vendedorModel{i}",
                    Email = "vendeor@teste.com.br",
                    Senha = $"123456{i}"
                };
                vendedoresModel.Add(vendedorModel);
            }
            return vendedoresModel;
        }

        public static VendedorModel PopulaVendedorModel()
        {
            return new VendedorModel()
            {
                Id = 1,
                Nome = "Test mock vendedorModel",
                Email = "vendeor@teste.com.br",
                Senha = "123456"
            };
        }

        private List<VendedorDto> PopulaAllVendedoresDto()
        {
            for (int i = 1; i < 3; i++)
            {
                vendedorDto = new VendedorDto()
                {
                    Id = i,
                    Nome = $"Test mock vendedorModel{i}",
                    Email = "vendeor@teste.com.br",
                    Senha = $"123456{i}",
                    ComparaSenha = $"123456{i}"
                };
                vendedoresDto.Add(vendedorDto);
            }
            return vendedoresDto;
        }

        public static VendedorDto PopulaVendedorDto()
        {
            return new VendedorDto()
            {
                Id = 1,
                Nome = "Test mock vendedorModel",
                Email = "vendeor@teste.com.br",
                Senha = "123456",
                ComparaSenha = "123456"
            };
        }
        #endregion PopulaClasses
    }
}
