using Hospital.ProductCatalog.BusinessLogic.Categories.Queries;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductCatalogContext _db;
        private readonly IMediator _mediator;

        public CategoriesController(ProductCatalogContext db, IMediator mediator)
        {
            _db = db;
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
        public async Task<ActionResult<Category>> Post(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { category.Code }, category);
        }

        [HttpPut("{code}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Category>> Put(int code, Category category)
        {
            if (code != category.Code)
            {
                return BadRequest();
            }

            var dbCategory = await _db.Categories.FirstOrDefaultAsync(x => x.Code == code);
            if (dbCategory == null)
            {
                return NotFound();
            }

            dbCategory.Description = category.Description;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{code}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<Category>> Delete(int code)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Code == code);

            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
