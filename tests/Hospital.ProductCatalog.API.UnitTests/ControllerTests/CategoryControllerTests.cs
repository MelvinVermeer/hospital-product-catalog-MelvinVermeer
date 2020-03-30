using Hospital.ProductCatalog.API.Controllers;
using Hospital.ProductCatalog.BusinessLogic.Categories.Queries;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.UnitTests.ControllerTests
{
    [TestClass]
    public class CategoryControllerTests
    {
        private readonly Category _category = new Category { Code = 1, Description = "Consumables" };

        [TestMethod]
        public async Task Get_Should_Send_Query_GetAll()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(null, mediatorMock.Object);

            await controller.Get();

            mediatorMock.Verify(m => m.Send(It.IsAny<GetAll>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Get_Should_Send_Query_GetByCode_If_Code_Provided()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(null, mediatorMock.Object);
            var code = 1;

            await controller.Get(code);

            mediatorMock.Verify(m => m.Send(It.Is<GetByCode>(x => x.Code == code), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Get_Should_Return_NotFound_If_Mediator_Throws_NotFound()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetByCode>(), It.IsAny<CancellationToken>()))
                .Throws(new NotFoundException("a", 1));
            var controller = new CategoriesController(null, mediatorMock.Object);
            var code = 1;

            var result = (await controller.Get(code)).Result;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
