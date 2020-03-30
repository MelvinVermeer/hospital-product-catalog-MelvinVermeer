using Hospital.ProductCatalog.BusinessLogic.Categories.Commands;
using Hospital.ProductCatalog.BusinessLogic.Categories.Queries;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _mediator.Send(new GetAll());
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> Get(int code)
        {
            try
            {
                return await _mediator.Send(new GetByCode(code));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Category>> Post(CreateCategory createCategory)
        {
            var code = await _mediator.Send(createCategory);
            return CreatedAtAction(nameof(Get), new { code });
        }

        [HttpPut("{code}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Category>> Put(int code, UpdateCategory updateCategory)
        {
            if (code != updateCategory.Code)
            {
                return BadRequest();
            }

            try
            {
                await _mediator.Send(updateCategory);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Category>> Delete(int code)
        {
            try
            {
                await _mediator.Send(new DeleteCategory(code));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
