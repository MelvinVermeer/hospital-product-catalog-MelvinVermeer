using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Commands
{
    public class DeleteProduct : IRequest
    {
        public int Code { get; private set; }

        public DeleteProduct(int code)
        {
            Code = code;
        }
    }

    // This Unit.Value represents a void type, since System.Void is not a valid return type in C#
    // Part of the MediatR package
    public class DeleteProductHandler : IRequestHandler<DeleteProduct, Unit>
    {
        private readonly ProductCatalogContext _context;

        public DeleteProductHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProduct request, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(request.Code);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Code);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
            return Unit.Value; 
        }
    }
}
