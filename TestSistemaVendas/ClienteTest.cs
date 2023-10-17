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
    public class ClienteTest
    {
        private ClienteController _controller;
        private Mock<IClienteRepository> _repository;
        private Mock<IMapper> _mapper;

        List<ClienteModel> clientesModel = new List<ClienteModel>();
        ClienteModel clienteModel = new ClienteModel();
        List<ClienteDto> clientesDto = new List<ClienteDto>();
        ClienteDto clienteDto = new ClienteDto();        

        [SetUp]
        [Category("Steup")]
        public void SetUp()
        {
            _repository = new Mock<IClienteRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new ClienteController(_repository.Object, _mapper.Object);

            clientesModel = PopulaAllClientesModel();
            clienteModel = PopulaClienteModel();
            clientesDto = PopulaAllClientesDto();
            clienteDto = PopulaClienteDto();

            //Arrange
            _repository.Setup(x => x.GetAllClientes()).ReturnsAsync(clientesModel);
            _mapper.Setup(x => x.Map<List<ClienteDto>>(clientesModel)).Returns(clientesDto);
            _repository.Setup(x => x.GetClientePorId(clienteDto.Id)).ReturnsAsync(clienteModel);
            _mapper.Setup(x => x.Map<ClienteDto>(clienteModel)).Returns(clienteDto);
            _mapper.Setup(x => x.Map<ClienteModel>(clienteDto)).Returns(clienteModel);
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
            Assert.That(result.Model, Is.EqualTo(clientesDto));

            var clientesNotNull = result.Model as List<ClienteDto>;
            Assert.That(clientesNotNull.Count, Is.EqualTo(clientesDto.Count)); 
        }
        
        [Test]
        [Category("Add")]
        public void Add()
        {
            //Arrange já declarado SetUp
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
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.Post(clienteDto) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }

        [Test]
        [Category("Update")]
        public async Task Update()
        {
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.Update(clienteDto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(clienteDto));

            var clienteNotNull = result.Model as ClienteDto;
            Assert.That(clienteNotNull, Is.EqualTo(clienteDto));
        }

        [Test]
        [Category("Update")]
        public async Task Put()
        {
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.Put(clienteDto) as RedirectToActionResult;

            ////Assert
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
            var result = await _controller.Delete(clienteDto.Id) as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.That(result.Model, Is.EqualTo(clienteDto));

            var clienteNotNull = result.Model as ClienteDto;
            Assert.That(clienteNotNull, Is.EqualTo(clienteDto));
        }

        [Test]
        [Category("Delete")]
        public async Task DeleteConfirm()
        {
            //Arrange já declarado SetUp
            //Act
            var result = await _controller.DeleteConfirm(clienteDto.Id) as RedirectToActionResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActionName);
            Assert.That(result.ActionName, Is.EqualTo("Index"));
        }
        

        #region PopulaClasses
        private List<ClienteModel> PopulaAllClientesModel()
        {
            for (int i = 1; i < 3; i++)
            {
                clienteModel = new ClienteModel()
                {
                    Id = i,
                    Nome = $"Test mock com {i}",
                    CpfCnpj = $"1234567891013{i}",
                    Email = "teste@mock.com.br",
                    Senha = $"123456{i}"
                };
                clientesModel.Add(clienteModel);
            }
            return clientesModel;
        }

        private ClienteModel PopulaClienteModel()
        {
            for (int i = 0; i < 1; i++)
            {
                clienteModel = new ClienteModel()
                {
                    Id = clientesModel[i].Id,
                    Nome = clientesModel[i].Nome,
                    CpfCnpj = clientesModel[i].CpfCnpj,
                    Email = clientesModel[i].Email,
                    Senha = clientesModel[i].Senha
                };
            }
            return clienteModel;
        }

        private List<ClienteDto> PopulaAllClientesDto()
        {
            for (int i = 1; i < 3; i++)
            {
                clienteDto = new ClienteDto()
                {
                    Id = i,
                    Nome = $"Test mock com {i}",
                    CpfCnpj = $"1234567891013{i}",
                    Email = "teste@mock.com.br",
                    Senha = $"123456{i}",
                    ComparaSenha = $"123456{i}"
                };
                clientesDto.Add(clienteDto);
            }
            return clientesDto;
        }

        private ClienteDto PopulaClienteDto()
        {
            for (int i = 0; i < 1; i++)
            {
                clienteDto = new ClienteDto()
                {
                    Id = clientesDto[i].Id,
                    Nome = clientesDto[i].Nome,
                    CpfCnpj = clientesDto[i].CpfCnpj,
                    Email = clientesDto[i].Email,
                    Senha = clientesDto[i].Senha,
                    ComparaSenha = clientesDto[i].ComparaSenha
                };
            }
            return clienteDto;
        }
        #endregion PopulaClasses
    }
}
