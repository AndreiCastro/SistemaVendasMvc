using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SistemaVendas.Controllers;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSistemaVendas
{
    [TestFixture]
    public class VendedorTest
    {
        private VendedorController _controller;
        private Mock<IVendedorRepository> _repository;
        List<VendedorModel> vendedores = new List<VendedorModel>();
        VendedorModel vendedor = new VendedorModel();

        [SetUp]
        [Category("SetUp")]
        public void SetUp()
        {
            _repository = new Mock<IVendedorRepository>();
            _controller = new VendedorController(_repository.Object);
            vendedores = PopulaAllVendedores();
            vendedor = PopulaVendedor();

            //Arrange
            _repository.Setup(x => x.GetAllVendedores()).ReturnsAsync(vendedores);
            _repository.Setup(x => x.GetVendedorPorId(vendedor.Id)).ReturnsAsync(vendedor);
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
            Assert.IsNotNull(vendedores);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendedores));

            var vendedoresNotNull = result.Model as List<VendedorModel>;
            Assert.That(vendedoresNotNull.Count, Is.EqualTo(vendedores.Count));

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
            var result = (RedirectToActionResult)await _controller.Post(vendedor);

            //Assert
            Assert.IsNotNull(vendedor);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Update")]
        public async Task Update()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Update(vendedor.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(vendedor);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendedor));

            var vendedorNotNull = result.Model as VendedorModel;
            Assert.That(vendedorNotNull, Is.EqualTo(vendedor));
        }

        [Test]
        [Category("Update")]
        public async Task Put()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = (RedirectToActionResult)await _controller.Put(vendedor);

            //Assert
            Assert.IsNotNull(vendedor);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Delete")]
        public async Task Delete()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Delete(vendedor.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(vendedor);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendedor));

            var vendedorNotNull = result.Model as VendedorModel;
            Assert.That(vendedorNotNull, Is.EqualTo(vendedor));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = (RedirectToActionResult)await _controller.DeleteConfirm(vendedor.Id);

            //Assert
            Assert.IsNotNull(vendedor);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        private List<VendedorModel> PopulaAllVendedores()
        {
            for (int i = 1; i < 3; i++)
            {
                vendedor = new VendedorModel()
                {
                    Id = i,
                    Nome = $"Test mock vendedor{i}",
                    Email = "vendeor@teste.com.br",
                    Senha = $"123456{i}",
                    ComparaSenha = $"123456{i}"
                };
                vendedores.Add(vendedor);
            }
            return vendedores;
        }

        private VendedorModel PopulaVendedor()
        {
            return new VendedorModel()
            {
                Id = 1,
                Nome = "Test mock vendedor",
                Email = "vendeor@teste.com.br",
                Senha = "123456",
                ComparaSenha = "123456"
            };
        }
    }
}
