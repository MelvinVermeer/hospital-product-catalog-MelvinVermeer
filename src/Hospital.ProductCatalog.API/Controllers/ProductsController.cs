using Hospital.ProductCatalog.BusinessLogic.Products.Commands;
using Hospital.ProductCatalog.BusinessLogic.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            return await _mediator.Send(new GetAll());
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> Get(int code)
        {
            return await _mediator.Send(new GetByCode(code));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Post(CreateProduct createProduct)
        {
            var code = await _mediator.Send(createProduct);
            return CreatedAtAction(nameof(Get), new { code }, null);
        }

        [HttpPut("{code}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int code, UpdateProduct updateProduct)
        {
            if (code != updateProduct.Code)
            {
                return BadRequest();
            }

            await _mediator.Send(updateProduct);
            return NoContent();
        }

        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int code)
        {
            await _mediator.Send(new DeleteProduct(code));
            return NoContent();
        }
    }
}
