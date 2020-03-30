using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Commands
{
    public class CreateProduct : IRequest<int>
    {
        public string Description { get; set; }
    }

    public class CreateProductHandler : IRequestHandler<CreateProduct, int>
    {
        private readonly ProductCatalogContext _context;

        public CreateProductHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateProduct request, CancellationToken cancellationToken = default)
        {
            var product = new Product
            {
                Description = request.Description
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Code;
        }
    }
}
