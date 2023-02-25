using AutoMapper;
using back_template_mongo.BLL;
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
        private Mock<IMapper> _mapper;
        private Mock<ILoggerManager> _logger;
        private Mock<ILivroBll> _livroBll;
        private Livro _livro;


        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILoggerManager>();
            _livroBll = new Mock<ILivroBll>();
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
            var result = _livroController.Inserir(_livro);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var response = okResult.Value as ApiResponse;
            Assert.That(response.StatusCode, Is.EqualTo(200));
            Assert.That(response.Message, Is.EqualTo("Livro inserido com sucesso."));
        }
    }
}