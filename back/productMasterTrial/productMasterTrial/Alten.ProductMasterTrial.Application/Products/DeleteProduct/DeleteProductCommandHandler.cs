using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Application.Specifications.Products;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Application.Products.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
    {
        private readonly IRepository<Product> _productRepository;
        public DeleteProductCommandHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(DeleteProductCommand command)
        {
            var getProductByCodeSpecification = new ProductByCodeSpecification(command.code);
            var product = await _productRepository.FirstOrDefaultAsync(getProductByCodeSpecification);

            if (product == null)
            {
                return Result.Failure(DomainErrors.Product.NotFound);
            }

            await _productRepository.DeleteAsync(product);

            return Result.Success();
        }
    }
}
