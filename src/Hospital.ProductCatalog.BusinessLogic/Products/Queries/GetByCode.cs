using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Queries
{
    public class GetByCode : IRequest<Product>
    {
        public int Code { get; private set; }

        public GetByCode(int code)
        {
            Code = code;
        }
    }

    public class GetByCodeQueryHandler : IRequestHandler<GetByCode, Product>
    {
        private readonly ProductCatalogContext _context;

        public GetByCodeQueryHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> Handle(GetByCode request, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(request.Code);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Code);
            }

            return product;
        }
    }
}
