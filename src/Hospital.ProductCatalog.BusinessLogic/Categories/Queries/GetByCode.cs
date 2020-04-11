using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetByCodeQueryHandler> _logger;

        public GetByCodeQueryHandler(ProductCatalogContext context, ILogger<GetByCodeQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Category> Handle(GetByCode request, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(request.Code);

            if (category == null)
            {
                var exception = new NotFoundException(nameof(Category), request.Code);
                _logger.LogWarning(exception.Message);
                throw exception;
            }

            return category;
        }
    }
}
