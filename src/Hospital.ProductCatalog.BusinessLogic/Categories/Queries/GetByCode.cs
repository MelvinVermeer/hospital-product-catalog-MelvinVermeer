using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Categories.Queries
{
    public class GetByCode : IRequest<Category>
    {
        public int Code { get; private set; }

        public GetByCode(int code)
        {
            Code = code;
        }
    }

    public class GetByCodeQueryHandler : IRequestHandler<GetByCode, Category>
    {
        private readonly ProductCatalogContext _context;

        public GetByCodeQueryHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(GetByCode request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Code);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Code);
            }

            return category;
        }
    }
}
