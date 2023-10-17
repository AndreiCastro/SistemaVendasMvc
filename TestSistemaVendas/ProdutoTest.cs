using AutoMapper;
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
    public class ProdutoTest
    {
        private ProdutoController _controller;
        private Mock<IProdutoRepository> _repository;
        private Mock<IMapper> _mapper;

        List<ProdutoModel> produtosModel = new List<ProdutoModel>();
        ProdutoModel produtoModel = new ProdutoModel();
        List<ProdutoDto> produtosDto = new List<ProdutoDto>();
        ProdutoDto produtoDto = new ProdutoDto();

        [SetUp]
        [Category("SetUp")]
        public void SetUp()
        {
            _repository = new Mock<IProdutoRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new ProdutoController(_repository.Object, _mapper.Object);
            
            produtosModel = PopulaAllProdutosModel();
            produtoModel = PopulaProdutoModel();
            produtosDto = PopulaAllProdutosDto();
            produtoDto = PopulaProdutoDto();

            //Arrange
            _repository.Setup(x => x.GetAllProdutos()).ReturnsAsync(produtosModel);
            _mapper.Setup(x => x.Map<List<ProdutoDto>>(produtosModel)).Returns(produtosDto);
            _mapper.Setup(x => x.Map<ProdutoDto>(produtoModel)).Returns(produtoDto);
            _repository.Setup(x => x.GetProdutoPorId(produtoDto.Id)).ReturnsAsync(produtoModel);
            _mapper.Setup(x => x.Map<ProdutoModel>(produtoDto)).Returns(produtoModel);
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
            Assert.That(result.Model, Is.EqualTo(produtosDto));

            var produtosNotNull = result.Model as List<ProdutoDto>;
            Assert.That(produtosNotNull.Count, Is.EqualTo(produtosDto.Count));
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
            var result = await _controller.Post(produtoDto) as RedirectToActionResult;

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
            var result = await _controller.Update(produtoDto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(produtoDto));

            var produtoNotNull = result.Model as ProdutoDto;
            Assert.That(produtoNotNull, Is.EqualTo(produtoDto));
        }

        [Test]
        [Category("Update")]
        public async Task Put()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Put(produtoDto) as RedirectToActionResult;

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
            var result = await _controller.Delete(produtoDto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(produtoDto));

            var produtoNotNull = result.Model as ProdutoDto;
            Assert.That(produtoNotNull, Is.EqualTo(produtoDto));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.DeleteConfirm(produtoDto.Id) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        #region Popula Classes
        private List<ProdutoModel> PopulaAllProdutosModel()
        {
            for (int i = 1; i < 3; i++)
            {
                produtoModel = new ProdutoModel()
                {
                    Id = i,
                    Nome = $"Test mock produtoModel {i}",
                    Descricao = $"Test mock descricao {i}",
                    PrecoUnitario = 100.00M,
                    QuantidadeEstoque = 100,
                    UnidadeMedida = "10",
                    Link_Foto = "teste mock link"
                };
                produtosModel.Add(produtoModel);
            }
            return produtosModel;
        }

        public static ProdutoModel PopulaProdutoModel()
        {
            return new ProdutoModel()
            {
                Id = 1,
                Nome = "test mock produtoModel",
                Descricao = "test mock produtoModel descricao",
                PrecoUnitario = 100.00M,
                QuantidadeEstoque = 100,
                UnidadeMedida = "10",
                Link_Foto = "teste mock link"
            };
        }

        private List<ProdutoDto> PopulaAllProdutosDto()
        {
            for (int i = 1; i < 3; i++)
            {
                produtoDto = new ProdutoDto()
                {
                    Id = i,
                    Nome = $"Test mock produtoModel {i}",
                    Descricao = $"Test mock descricao {i}",
                    PrecoUnitario = 100.00M,
                    QuantidadeEstoque = 100,
                    UnidadeMedida = "10",
                    Link_Foto = "teste mock link"
                };
                produtosDto.Add(produtoDto);
            }
            return produtosDto;
        }

        public static ProdutoDto PopulaProdutoDto()
        {
            return new ProdutoDto()
            {
                Id = 1,
                Nome = "test mock produtoModel",
                Descricao = "test mock produtoModel descricao",
                PrecoUnitario = 100.00M,
                QuantidadeEstoque = 100,
                UnidadeMedida = "10",
                Link_Foto = "teste mock link"
            };
        }
        #endregion PopulaClasses
    }
}
