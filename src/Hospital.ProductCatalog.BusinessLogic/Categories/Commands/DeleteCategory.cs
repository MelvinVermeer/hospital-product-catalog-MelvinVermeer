using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Categories.Commands
{
    public class DeleteCategory : IRequest
    {
        public int Code { get; private set; }

        public DeleteCategory(int code)
        {
            Code = code;
        }
    }

    // This Unit.Value represents a void type, since System.Void is not a valid return type in C#
    // Part of the MediatR package
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, Unit>
    {
        private readonly ProductCatalogContext _context;

        public DeleteCategoryHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(request.Code);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Code);
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            
            return Unit.Value; 
        }
    }
}
