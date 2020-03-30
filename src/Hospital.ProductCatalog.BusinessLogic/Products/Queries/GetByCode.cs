using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Queries
{
    public class GetByCode : IRequest<ProductDTO>
    {
        public int Code { get; private set; }

        public GetByCode(int code)
        {
            Code = code;
        }
    }

    public class GetByCodeQueryHandler : IRequestHandler<GetByCode, ProductDTO>
    {
        private readonly ProductCatalogContext _context;
        private readonly IMapper _mapper;

        public GetByCodeQueryHandler(ProductCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDTO> Handle(GetByCode request, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products
                .Include(x => x.Barcodes)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Code == request.Code);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Code);
            }

            return _mapper.Map<ProductDTO>(product);
        }
    }
}
