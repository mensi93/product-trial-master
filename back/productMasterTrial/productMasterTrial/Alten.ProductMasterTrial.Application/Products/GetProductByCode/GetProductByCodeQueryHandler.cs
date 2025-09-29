using Alten.ProductMaster.Application.Products.ProductResponses;
using Alten.ProductMaster.Domain.ValueObjects;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Application.Specifications.Products;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Application.Products.GetProductByCode
{
    public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, Result<ProductResponse>>
    {
        private readonly IReadRepository<Product> _productRepository;
        public GetProductByCodeQueryHandler(IReadRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<ProductResponse>> Handle(GetProductByCodeQuery query)
        {
            var getProductByCodeSpecification = new ProductByCodeAndProjectToProductResponse(query.code);
            var product = await _productRepository.FirstOrDefaultAsync(getProductByCodeSpecification);

            if (product == null)
            {
                return Result.Failure<ProductResponse>(DomainErrors.Product.NotFound);
            }

            return product;
        }
    }
}
