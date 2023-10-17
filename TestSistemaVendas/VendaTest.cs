using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SistemaVendas.Controllers;
using SistemaVendas.Dtos;
using SistemaVendas.Models;
using SistemaVendas.Repositorys;
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
        private Mock<IMapper> _mapper;

        List<VendaModel> vendasModel = new List<VendaModel>();
        VendaModel vendaModel = new VendaModel();
        List<VendaDto> vendasDto = new List<VendaDto>();
        VendaDto vendaDto = new VendaDto();
        ProdutoModel produtoModel = new ProdutoModel();

        [SetUp]
        [Category("Steup")]
        public void Setup()
        {
            _repository = new Mock<IVendaRepository>();
            _mapper = new Mock<IMapper>();
            _clienteRepository = new Mock<IClienteRepository>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _contextAccessor = new Mock<IHttpContextAccessor>();
            _controller = new VendaController(_repository.Object, _clienteRepository.Object, _produtoRepository.Object, 
                _contextAccessor.Object, _mapper.Object);

            vendasModel = PopulaAllVendasModel();
            vendaModel = PopulaVendaModel();
            vendasDto = PopulaAllVendasDto();
            vendaDto = PopulaVendaDto();
            produtoModel = ProdutoTest.PopulaProdutoModel();

            //Arrange
            _repository.Setup(x => x.GetAllVendas()).ReturnsAsync(vendasModel);
            _repository.Setup(x => x.GetVendaPorId(vendaModel.Id)).ReturnsAsync(vendaModel);
            _contextAccessor.Setup(x => x.HttpContext.Session.Equals(1));
            _produtoRepository.Setup(x => x.GetProdutoPorId(produtoModel.Id)).ReturnsAsync(produtoModel);
            _produtoRepository.Setup(x => x.SaveChanges()).ReturnsAsync(true);
            _repository.Setup(x => x.SaveChanges()).ReturnsAsync(true);
            _mapper.Setup(x => x.Map<VendaDto>(vendaModel)).Returns(vendaDto);
            _mapper.Setup(x => x.Map<List<VendaDto>>(vendasModel)).Returns(vendasDto);
            _mapper.Setup(x => x.Map<VendaModel>(vendaDto)).Returns(vendaModel);
            
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
            Assert.That(result.Model, Is.EqualTo(vendasDto));

            var vendasNotNull = result.Model as List<VendaDto>;
            Assert.That(vendasNotNull.Count, Is.EqualTo(vendasDto.Count));
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
            var result = await _controller.Post(vendaDto) as RedirectToActionResult;

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
            var result = await _controller.Delete(vendaDto.Id) as ViewResult; 

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendaDto));

            var vendaNotNull = result.Model as VendaDto;
            Assert.That(vendaNotNull, Is.EqualTo(vendaDto));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado SetUp
            //Act            
            var result = await _controller.DeleteConfirm(vendaDto.Id) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        #region PopulaClasse
        public static List<VendaModel> PopulaAllVendasModel()
        {
            var vendasModel = new List<VendaModel>();
            for (int i = 0; i < 2; i++)
            {
                vendasModel.Add(new VendaModel()
                {
                    Id = i + 1,
                    Data = DateTime.Now,
                    Total = 1000.00M,
                    QuantidadeProduto = 100 + i,
                    IdCliente = i + 1,
                    IdProduto = i + 1,
                    IdVendedor = i + 1
                });
            }
            return vendasModel;
        }

        private VendaModel PopulaVendaModel()
        {
            for (int i = 0; i < 1; i++)
            {
                vendaModel = new VendaModel()
                {
                    Id = vendasModel[i].Id,
                    Data = vendasModel[i].Data,
                    Total = vendasModel[i].Total,
                    QuantidadeProduto = vendasModel[i].QuantidadeProduto,
                    IdCliente = vendasModel[i].IdCliente,
                    IdVendedor = vendasModel[i].IdVendedor,
                    IdProduto = vendasModel[i].IdProduto
                };
            }
            return vendaModel;
        }

        public static List<VendaDto> PopulaAllVendasDto()
        {
            var vendasDto = new List<VendaDto>();
            for (int i = 0; i < 2; i++)
            {
                vendasDto.Add(new VendaDto()
                {
                    Id = i + 1,
                    Data = DateTime.Now,
                    Total = 1000.00M,
                    QuantidadeProduto = 100 + i                    
                });
            }
            return vendasDto;
        }

        private VendaDto PopulaVendaDto()
        {
            for (int i = 0; i < 1; i++)
            {
                vendaDto = new VendaDto()
                {
                    Id = vendasModel[i].Id,
                    Data = vendasModel[i].Data,
                    Total = vendasModel[i].Total,
                    QuantidadeProduto = vendasModel[i].QuantidadeProduto                    
                };
            }
            return vendaDto;
        }
        #endregion PopulaClasses
    }
}