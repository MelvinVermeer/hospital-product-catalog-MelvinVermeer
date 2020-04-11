using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetByCodeQueryHandler> _logger;

        public GetByCodeQueryHandler(ProductCatalogContext context, IMapper mapper, ILogger<GetByCodeQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDTO> Handle(GetByCode request, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products
                .Include(x => x.Barcodes)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Code == request.Code);

            if (product == null)
            {
                var exception = new NotFoundException(nameof(Product), request.Code);
                _logger.LogWarning(exception.Message);
                throw exception;
            }

            return _mapper.Map<ProductDTO>(product);
        }
    }
}
