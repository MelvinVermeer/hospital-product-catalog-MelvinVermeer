using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.ProductCatalog.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Queries
{
    public class GetAll : IRequest<List<ProductDTO>>
    {
    }

    public class GetAllQueryHandler : IRequestHandler<GetAll, List<ProductDTO>>
    {
        private readonly ProductCatalogContext _context;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(ProductCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> Handle(GetAll request, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include(x => x.Barcodes)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
