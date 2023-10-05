using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SistemaVendas.Controllers;
using SistemaVendas.Models;
using SistemaVendas.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestSistemaVendas
{
    [TestFixture]
    public class ProdutoTest
    {
        private ProdutoController _controller;
        private Mock<IProdutoRepository> _repository;
        List<ProdutoModel> produtos = new List<ProdutoModel>();
        ProdutoModel produto = new ProdutoModel();

        [SetUp]
        [Category("SetUp")]
        public void SetUp()
        {
            _repository = new Mock<IProdutoRepository>();
            _controller = new ProdutoController(_repository.Object);
            produtos = PopulaAllProdutos();
            produto = PopulaProduto();

            //Arrange
            _repository.Setup(x => x.GetAllProdutos()).ReturnsAsync(produtos);
            _repository.Setup(x => x.Add(produto));
            _repository.Setup(x => x.GetProdutoPorId(produto.Id)).ReturnsAsync(produto);
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
            Assert.IsNotNull(produtos);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(produtos));

            var produtosNotNull = result.Model as List<ProdutoModel>;
            Assert.That(produtosNotNull.Count, Is.EqualTo(produtos.Count));
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
            var result = (RedirectToActionResult) await _controller.Post(produto);

            //Assert
            Assert.IsNotNull(produto);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Update")]
        public async Task Update()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Update(produto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(produto);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(produto));

            var produtoNotNull = result.Model as ProdutoModel;
            Assert.That(produtoNotNull, Is.EqualTo(produto));
        }

        [Test]
        [Category("Update")]
        public async Task Put()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = (RedirectToActionResult) await _controller.Put(produto);

            //Assert
            Assert.IsNotNull(produto);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Delete")]
        public async Task Delete()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Delete(produto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(produto);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(produto));

            var produtoNotNull = result.Model as ProdutoModel;
            Assert.That(produtoNotNull, Is.EqualTo(produto));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = (RedirectToActionResult) await _controller.DeleteConfirm(produto.Id);

            //Assert
            Assert.IsNotNull(produto);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        private List<ProdutoModel> PopulaAllProdutos()
        {
            for (int i = 1; i < 3; i++)
            {
                produto = new ProdutoModel()
                {
                    Id = i,
                    Nome = $"Test mock produto {i}",
                    Descricao = $"Test mock descricao {i}",
                    PrecoUnitario = 100.00M,
                    QuantidadeEstoque = 100,
                    UnidadeMedida = "UN",
                    Link_Foto = "teste mock link"
                };
                produtos.Add(produto);
            }
            return produtos;
        }

        public static ProdutoModel PopulaProduto()
        {
            return new ProdutoModel()
            {
                Id = 1,
                Nome = "test mock produto",
                Descricao = "test mock produto descricao",
                PrecoUnitario = 100.00M,
                QuantidadeEstoque = 100,
                UnidadeMedida = "UN",
                Link_Foto = "teste mock link"
            };
        }
    }
}
