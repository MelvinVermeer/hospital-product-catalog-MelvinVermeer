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
    public class UpdateProduct : IRequest
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public List<string> Barcodes { get; set; }
        public int CategoryCode { get; set; }
        public int Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    // This Unit.Value represents a void type, since System.Void is not a valid return type in C#
    // Part of the MediatR package
    public class UpdateProductHandler : IRequestHandler<UpdateProduct,Unit>
    {
        private readonly ProductCatalogContext _context;
        private readonly IMapper _mapper;

        public UpdateProductHandler(ProductCatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProduct request, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(request.Code);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Code);
            }

            var category = await _context.Categories.FindAsync(request.CategoryCode);
            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryCode);
            }

            _mapper.Map(request, product);
            product.Category = category;

            await _context.SaveChangesAsync();
            
            return Unit.Value; 
        }
    }
}
