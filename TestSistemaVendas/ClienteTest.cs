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
    public class ClienteTest
    {
        private ClienteController _controller;
        private Mock<IClienteRepository> _repository;

        List<ClienteModel> clientes = new List<ClienteModel>();
        ClienteModel cliente = new ClienteModel();
        
        [SetUp]
        public void SetUp()
        {
            _repository = new Mock<IClienteRepository>();
            _controller = new ClienteController(_repository.Object);

            clientes = PopulaClientes();
            cliente = PopulaCliente();

            //Arrange
            _repository.Setup(x => x.GetAllClientes()).ReturnsAsync(clientes);
            _repository.Setup(x => x.SaveChanges()).ReturnsAsync(true);
            _repository.Setup(x => x.GetClientePorId(cliente.Id)).ReturnsAsync(cliente);
        }

        [Test]
        [Category("Index")]
        public async Task Index()
        {
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(clientes);
            Assert.That(result.Model, Is.EqualTo(clientes));

            var clienteNotNull = result.Model as List<ClienteModel>;
            Assert.That(clienteNotNull.Count, Is.EqualTo(clientes.Count)); 
        }

        [Test]
        [Category("Add")]
        public void Add()
        {
            //Arrange já declarado SetUp
            //Act
            var result = _controller.Add() as ViewResult;

            //Assert
            Assert.That(result.ViewName, Is.EqualTo("Add"));
        }

        [Test]
        [Category("Add")]
        public async Task Post()
        {
            //Arrange já declarado SetUp
            //Act
            var result = (RedirectToActionResult) await _controller.Post(cliente);

            //Assert
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Update")]
        public async Task Update()
        {
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.Update(cliente.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(cliente);
            Assert.That(result.Model, Is.EqualTo(cliente));

            var clienteNotNull = result.Model as ClienteModel;
            Assert.That(clienteNotNull, Is.EqualTo(cliente));
        }

        [Test]
        [Category("Update")]
        public async Task Put()
        {
            //Arrange já declarado SetUp
            //Act
            var result = (RedirectToActionResult) await _controller.Put(cliente);

            //Assert
            Assert.IsNotNull(cliente);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Delete")]
        public async Task Delete()
        {
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.Delete(cliente.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(cliente);
            Assert.That(result.Model, Is.EqualTo(cliente));

            var clienteNotNull = result.Model as ClienteModel;
            Assert.That(clienteNotNull, Is.EqualTo(cliente));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado SetUp
            //Act
            var result = (RedirectToActionResult)await _controller.DeleteConfirm(cliente.Id);

            //Assert
            Assert.IsNotNull(cliente);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        public List<ClienteModel> PopulaClientes()
        {
            for (int i = 0; i < 2; i++)
            {
                cliente = new ClienteModel()
                {
                    Id = i,
                    Nome = $"Test mock com {i}",
                    CpfCnpj = $"1234567891013{i}",
                    Email = "teste@mock.com.br",
                    Senha = $"123456{i}",
                    ComparaSenha = $"123456{i}"
                };
                clientes.Add(cliente);
            }
            return clientes;
        }

        public ClienteModel PopulaCliente()
        {
            for (int i = 0; i < 1; i++)
            {
                cliente = new ClienteModel()
                {
                    Id = clientes[i].Id,
                    Nome = clientes[i].Nome,
                    CpfCnpj = clientes[i].CpfCnpj,
                    Email = clientes[i].Email,
                    Senha = clientes[i].Senha,
                    ComparaSenha = clientes[i].ComparaSenha
                };
            }
            return cliente;
        }
    }
}
