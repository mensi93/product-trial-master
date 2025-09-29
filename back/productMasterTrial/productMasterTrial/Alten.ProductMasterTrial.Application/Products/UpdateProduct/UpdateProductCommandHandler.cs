using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Application.Specifications.Products;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Application.Products.UpdateProduct
{
    //todo : validation before update
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
    {
        private readonly IRepository<Product> _productRepository;
        public UpdateProductCommandHandler(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(UpdateProductCommand command)
        {
            var getProductByCodeSpecification = new ProductByCodeSpecification(command.code);
            var product = await _productRepository.FirstOrDefaultAsync(getProductByCodeSpecification);

            if (product == null) 
            {
                return Result.Failure(DomainErrors.Product.NotFound);
            }

            product.Update(command.name, command.description, command.image, command.price, command.quantity);

            await _productRepository.UpdateAsync(product);
            return Result.Success();
        }
    }
}
