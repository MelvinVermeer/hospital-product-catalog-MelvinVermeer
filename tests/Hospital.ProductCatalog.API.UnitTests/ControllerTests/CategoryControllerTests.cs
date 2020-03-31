using Hospital.ProductCatalog.API.Controllers;
using Hospital.ProductCatalog.BusinessLogic.Categories.Commands;
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
            var controller = new CategoriesController(mediatorMock.Object);

            await controller.Get();

            mediatorMock.Verify(m => m.Send(It.IsAny<GetAll>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Get_Should_Send_Query_GetByCode_If_Code_Provided()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(mediatorMock.Object);
            var code = 1;

            await controller.Get(code);

            mediatorMock.Verify(m => m.Send(It.Is<GetByCode>(x => x.Code == code), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Get_Should_Return_NotFound_If_Mediator_Throws_NotFound()
        {
            var code = 1;
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GetByCode>(), It.IsAny<CancellationToken>()))
                .Throws(new NotFoundException(nameof(Category), code));
            var controller = new CategoriesController(mediatorMock.Object);

            var result = (await controller.Get(code)).Result;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Post_Should_Send_CreateCategory_Command()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(mediatorMock.Object);
            var command = new CreateCategory();

            await controller.Post(command);

            mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Post_Should_Return_HttpCreated_With_CategoryCode()
        {
            var code = 1;
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<CreateCategory>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(code);
            var controller = new CategoriesController(mediatorMock.Object);

            var result = await controller.Post(new CreateCategory());

            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdAt = result as CreatedAtActionResult;
            Assert.AreEqual(nameof(CategoriesController.Get), createdAt.ActionName);
            Assert.AreEqual(new { code }.ToString(), createdAt.Value.ToString());
        }

        [TestMethod]
        public async Task Put_Should_Return_BadRequest_When_Code_Param_NotEquals_UpdateCommandCode()
        {
            var controller = new CategoriesController(null);

            var result = await controller.Put(1, new UpdateCategory { Code = 2 });

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task Put_Should_Send_UpdateCategory_Command()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(mediatorMock.Object);
            var command = new UpdateCategory { Code = 1 };

            await controller.Put(1, command);

            mediatorMock.Verify(m => m.Send(command, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Put_Should_Return_HttpNoContent()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(mediatorMock.Object);
            var command = new UpdateCategory { Code = 1 };

            var result = await controller.Put(1, command);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Put_Should_Return_NotFound_If_Mediator_Throws_NotFound()
        {
            var code = 1;
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<UpdateCategory>(), It.IsAny<CancellationToken>()))
                .Throws(new NotFoundException(nameof(Category), code));
            var controller = new CategoriesController(mediatorMock.Object);

            var result = await controller.Put(1, new UpdateCategory { Code = code });

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Delete_Should_Send_DeleteCategory_Command()
        {
            var code = 1;
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(mediatorMock.Object);

            await controller.Delete(code);

            mediatorMock.Verify(m => m.Send(It.Is<DeleteCategory>(x => x.Code == code), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task Delete_Should_Return_HttpNoContent()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = new CategoriesController(mediatorMock.Object);

            var result = await controller.Delete(1);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task Delete_Should_Return_NotFound_If_Mediator_Throws_NotFound()
        {
            var code = 1;
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<DeleteCategory>(), It.IsAny<CancellationToken>()))
                .Throws(new NotFoundException(nameof(Category), code));
            var controller = new CategoriesController(mediatorMock.Object);

            var result = await controller.Delete(code);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
