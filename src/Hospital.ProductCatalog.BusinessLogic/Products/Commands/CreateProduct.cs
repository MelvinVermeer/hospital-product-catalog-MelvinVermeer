using AutoMapper;
using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Commands
{
    public class CreateProduct : IRequest<int>
    {
        public string Description { get; set; }
        public List<string> Barcodes { get; set; }
        public int CategoryCode { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class CreateProductHandler : IRequestHandler<CreateProduct, int>
    {
        private readonly ProductCatalogContext _context;
        private readonly IMapper _mapper;

        public CreateProductHandler(ProductCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProduct request, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(request.CategoryCode);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryCode);
            }

            var product = _mapper.Map<Product>(request);
            product.Category = category;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Code;
        }
    }
}
