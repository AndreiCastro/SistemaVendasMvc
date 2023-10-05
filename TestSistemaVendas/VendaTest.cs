using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SistemaVendas.Controllers;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestSistemaVendas
{
    [TestFixture]
    public class VendaTest
    {
        private VendaController _controller;
        private Mock<IVendaRepository> _repository;
        private Mock<IClienteRepository> _clienteRepository;
        private Mock<IProdutoRepository> _produtoRepository;
        private Mock<IHttpContextAccessor> _contextAccessor;
        List<VendaModel> vendas = new List<VendaModel>();
        VendaModel venda = new VendaModel();
        ProdutoModel produto = new ProdutoModel();

        [SetUp]
        [Category("Steup")]
        public void Setup()
        {
            _repository = new Mock<IVendaRepository>();
            _clienteRepository = new Mock<IClienteRepository>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _contextAccessor = new Mock<IHttpContextAccessor>();
            _controller = new VendaController(_repository.Object, _clienteRepository.Object, _produtoRepository.Object, _contextAccessor.Object);
            vendas = PopulaAllVendas();
            venda = PopulaVenda();
            produto = ProdutoTest.PopulaProduto();

            //Arrange
            _repository.Setup(x => x.GetAllVendas()).ReturnsAsync(vendas);
            _repository.Setup(x => x.GetVendaPorId(venda.Id)).ReturnsAsync(venda);
            _contextAccessor.Setup(x => x.HttpContext.Session.Equals(1));
            _produtoRepository.Setup(x => x.GetProdutoPorId(produto.Id)).ReturnsAsync(produto);
            _produtoRepository.Setup(x => x.SaveChanges()).ReturnsAsync(true);
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
            Assert.That(result.Model, Is.EqualTo(vendas));

            var vendasNotNull = result.Model as List<VendaModel>;
            Assert.That(vendasNotNull.Count, Is.EqualTo(vendas.Count));
        }

        [Test]
        [Category("Add")]
        public async Task Add()
        {
            //Act            
            var result = await _controller.Add() as ViewResult;

            //Assert
            Assert.IsNotNull(result.ViewName);
            Assert.That(result.ViewName, Is.EqualTo("Add"));
        }

        [Test]
        [Category("Add")]
        public async Task Post()
        {
            //Arrange já declarado SetUp
            //Act            
            var result = await _controller.Post(venda) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));            
        }

        [Test]
        [Category("Delete")]
        public async Task Delete()
        {
            //Arrange já declarado SetUp
            //Act            
            var result = await _controller.Delete(venda.Id) as ViewResult; 

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(venda));

            var vendaNotNull = result.Model as VendaModel;
            Assert.That(vendaNotNull, Is.EqualTo(venda));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado SetUp
            //Act            
            var result = await _controller.DeleteConfirm(venda.Id) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        private List<VendaModel> PopulaAllVendas()
        {
            for (int i = 0; i < 2; i++)
            {
                vendas.Add(new VendaModel()
                {
                    Id = i + 1,
                    Data = DateTime.Now,
                    Total = 1000.00M,
                    Quantidade_Produto = 100 + i,
                    IdCliente = i + 1,
                    IdProduto = i + 1,
                    IdVendedor = i + 1
                });
            }
            return vendas;
        }

        private VendaModel PopulaVenda()
        {
            for (int i = 0; i < 1; i++)
            {
                venda = new VendaModel()
                {
                    Id = vendas[i].Id,
                    Data = vendas[i].Data,
                    Total = vendas[i].Total,
                    Quantidade_Produto = vendas[i].Quantidade_Produto,
                    IdCliente = vendas[i].IdCliente,
                    IdVendedor = vendas[i].IdVendedor,
                    IdProduto = vendas[i].IdProduto
                };
            }
            return venda;
        }
    }
}