using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Queries
{
    public class GetAll : IRequest<List<Product>>
    {
    }

    public class GetAllQueryHandler : IRequestHandler<GetAll, List<Product>>
    {
        private readonly ProductCatalogContext _context;

        public GetAllQueryHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Handle(GetAll request, CancellationToken cancellationToken = default)
        {
            return await _context.Products.ToListAsync();
        }
    }
}
