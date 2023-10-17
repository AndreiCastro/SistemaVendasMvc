using AutoMapper;
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
    public class RelatorioTest
    {
        private RelatorioController _controller;
        private Mock<IVendaRepository> _vendaRepository;
        private Mock<IGraficoVendaRepository> _repository;
        private Mock<IMapper> _mapper;

        List<VendaDto> vendasDto = new List<VendaDto>();
        List<VendaModel> vendasModel = new List<VendaModel>();
        List<GraficoVendaModel> graficoVendasModel = new List<GraficoVendaModel>();

        [SetUp]
        [Category("SetUp")]
        public void Setup() 
        { 
            _vendaRepository = new Mock<IVendaRepository>();
            _repository = new Mock<IGraficoVendaRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new RelatorioController(_vendaRepository.Object, _repository.Object, _mapper.Object);

            vendasModel = VendaTest.PopulaAllVendasModel();
            vendasDto = VendaTest.PopulaAllVendasDto();

            //Arrange
            _vendaRepository.Setup(x => x.GetVendaPorPeriodo(new DateTime(2023, 1, 1), new DateTime(2023, 12, 12))).ReturnsAsync(vendasModel);
            _vendaRepository.Setup(x => x.GetAllVendas()).ReturnsAsync(vendasModel);
            _repository.Setup(x => x.GetSomaProdutoVendido()).ReturnsAsync(graficoVendasModel);
            _mapper.Setup(x => x.Map<List<VendaDto>>(vendasModel)).Returns(vendasDto);
        }

        [Test]
        [Category("Index")]
        public void Index()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = _controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewName);
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("VendaPeriodo")]
        public async Task VendaPorPeriodo()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.VendaPeriodo(new DateTime(2023, 1, 1), new DateTime(2023, 12, 12)) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(vendasDto));

            var vendasNotNull = result.Model as List<VendaDto>;
            Assert.That(vendasNotNull, Is.EqualTo(vendasDto));
        }

        [Test]
        [Category("Grafico")]
        public async Task Grafico()
        {
            //Arrange já declarado no SetUp
            //Act
            var result = await _controller.Grafico() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewName);
            Assert.That(result.ViewName, Is.EqualTo("Grafico"));
        }
    }
}
