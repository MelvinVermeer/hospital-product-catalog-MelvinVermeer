using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Categories.Queries
{
    public class GetAll : IRequest<List<Category>>
    {
    }

    public class GetAllQueryHandler : IRequestHandler<GetAll, List<Category>>
    {
        private readonly ProductCatalogContext _context;

        public GetAllQueryHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Handle(GetAll request, CancellationToken cancellationToken = default)
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
