﻿using Hospital.ProductCatalog.BusinessLogic.Exceptions;
using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Products.Commands
{
    public class UpdateProduct : IRequest
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }

    // This Unit.Value represents a void type, since System.Void is not a valid return type in C#
    // Part of the MediatR package
    public class UpdateProductHandler : IRequestHandler<UpdateProduct,Unit>
    {
        private readonly ProductCatalogContext _context;

        public UpdateProductHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProduct request, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(request.Code);

            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Code);
            }

            product.Description = request.Description;

            await _context.SaveChangesAsync();
            
            return Unit.Value; 
        }
    }
}
