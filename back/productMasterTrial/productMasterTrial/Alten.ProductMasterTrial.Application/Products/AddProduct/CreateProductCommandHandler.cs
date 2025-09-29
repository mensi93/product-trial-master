using Alten.ProductMaster.Application.Specifications.Products;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Application.Specifications.Products;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Application.Products.AddProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly IRepository<Product> _productrepository;
        public CreateProductCommandHandler(
            IRepository<Product> productrepository)
        {
            _productrepository = productrepository;
        }
        public async Task<Result> Handle(CreateProductCommand command)
        {
            var productResult = Product.Create
                 (
                   command.code
                 , command.name
                 , command.description
                 , command.image
                 , command.price
                 , command.quantity
                 , command.category
                 , command.internalReference
                 , command.shellId);


            if (productResult.IsFailure)
            {
                return Result.Failure(productResult.Error);
            }

            if (await _productrepository.AnyAsync(new ProductByCodeSpecification(command.code)))
            {
                return Result.Failure(DomainErrors.Product.CodeAlreadyInUse);
            }

            if (await _productrepository.AnyAsync(new ProductByInternalReferenceSpecification(command.internalReference)))
            {
                return Result.Failure(DomainErrors.Product.InternalReferenceAlreadyInUse);
            }

            if (await _productrepository.AnyAsync(new ProductByShellIdSpecification(command.shellId)))
            {
                return Result.Failure(DomainErrors.Product.ShellIdAlreadyInUse);
            }

            await _productrepository.AddAsync(productResult.Value);

            return Result.Success();
        }
    }
}
