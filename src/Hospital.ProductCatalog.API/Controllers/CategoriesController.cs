using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
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

        public CategoriesController(ProductCatalogContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            return await _db.Categories.ToListAsync();
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Category>> Get(int code)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Code == code);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post(Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { category.Code }, category);
        }

        [HttpPut("{code}")]
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
