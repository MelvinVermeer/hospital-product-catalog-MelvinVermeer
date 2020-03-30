using Hospital.ProductCatalog.DataAccess;
using Hospital.ProductCatalog.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hospital.ProductCatalog.BusinessLogic.Categories.Commands
{
    public class CreateCategory : IRequest<int>
    {
        public string Description { get; set; }
    }

    public class CreateCategoryHandler : IRequestHandler<CreateCategory, int>
    {
        private readonly ProductCatalogContext _context;

        public CreateCategoryHandler(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Description = request.Description
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync(cancellationToken);

            return category.Code;
        }
    }
}
