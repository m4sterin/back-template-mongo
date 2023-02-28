using AutoMapper;
using back_template_mongo.BLL;
using back_template_mongo.BLL.Exceptions;
using back_template_mongo.Controllers;
using back_template_mongo.DAL.Models;
using back_template_mongo.Extensions.Responses;
using back_template_mongo.Utils;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
namespace Tests.Controllers
{

    [TestFixture]
    public class LivroControllerTests
    {

        private LivroController _livroController;
         private readonly Mock<IMapper> _mapper = new();
        private readonly Mock<ILoggerManager> _logger = new();
        private readonly Mock<ILivroBll> _livroBll = new();
        private Livro _livro;


        [SetUp]
        public void Setup()
        {
            _livroController = new LivroController(_livroBll.Object, _logger.Object, _mapper.Object);

            _livro = new Livro
            {
                id = "1",
                ano = 2020,
                autor = "Autor",
                descricao = "Descrição",
                editora = "Editora",
                preco = 10,
                titulo = "Título",
                qtd_paginas = 100,
                sinopse = "Sinopse"
            };

        }

        [Test]
        public void Inserir_LivroValido_RetornaOk()
        {
            _livroBll.Setup(x => x.Inserir(_livro)).Verifiable();

            var result = _livroController.Inserir(_livro);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse;
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Message, Is.EqualTo("Livro inserido com sucesso."));

            _livroBll.Verify(x => x.Inserir(_livro), Times.Once());
        }

        [Test]
        public void Inserir_LivroComCampoObrigatorioNulo_RetornaBadRequest()
        {
            // Arrange
            _livro.editora = null; // simula um campo obrigatório nulo
            _livroBll.Setup(x => x.Inserir(_livro)).Throws(new ObrigatoryFieldNotNullException("Campo obrigatório nulo"));

            // Act
            var result = _livroController.Inserir(_livro);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            var response = badRequestResult.Value as ApiResponse;
            Assert.That(response.StatusCode, Is.EqualTo(402));
            Assert.That(response.Message, Is.EqualTo("Campo obrigatório nulo"));
        }

        [Test]
        public void Inserir_LivroJaExistente_RetornaBadRequest()
        {
            _livroBll.Setup(x => x.Inserir(_livro)).Throws(new AlreadyExistsException("Livro já existente"));

            var result = _livroController.Inserir(_livro);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            var response = badRequestResult.Value as ApiResponse;
            Assert.That(response.StatusCode, Is.EqualTo(403));
            Assert.That(response.Message, Is.EqualTo("Livro já existente"));
        }

        [Test]
        public void Inserir_QuandoLancaExcecao_RetornaBadRequestComStatusCode500(){
            _livroBll.Setup(x => x.Inserir(_livro)).Throws(new System.Exception("Erro ao inserir livro"));

            var result = _livroController.Inserir(_livro);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            var response = badRequestResult.Value as ApiResponse;
            Assert.That(response.StatusCode, Is.EqualTo(500));
            Assert.That(response.Message, Is.EqualTo("Erro ao inserir livro"));
        }
    }
}