using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Categories.Commands
{
    public class UpdateCategory : IRequest
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }

    // This Unit.Value represents a void type, since System.Void is not a valid return type in C#
    // Part of the MediatR package
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory,Unit>
    {
        private readonly ProductCatalogContext _context;

        public UpdateCategoryHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategory request, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(request.Code);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Code);
            }

            category.Description = request.Description;

            await _context.SaveChangesAsync();
            
            return Unit.Value; 
        }
    }
}
