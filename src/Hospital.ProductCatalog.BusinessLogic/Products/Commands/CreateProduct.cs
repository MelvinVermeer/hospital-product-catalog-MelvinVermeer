using AutoMapper;
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
        public int Code { get; set; }
        public string Description { get; set; }
        public List<string> Barcodes { get; set; }
        public Category Category { get; set; }
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
            var product = _mapper.Map<Product>(request);
            
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            
            return product.Code;
        }
    }
}
