using Alten.ProductMaster.Application.Common.Pagination;
using Alten.ProductMaster.Application.Products.ProductResponses;
using Alten.ProductMaster.Application.Specifications.Products;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Domain.Entities;
namespace Alten.ProductMaster.Application.Products.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, Result<PaginiatedList<ProductResponse>>>
    {
        private readonly IReadRepository<Product> _productRepository;
        public GetProductListQueryHandler(IReadRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<PaginiatedList<ProductResponse>>> Handle(GetProductListQuery request)
        { 
            var getProductsPaginatedSpecification  = new ProductsPaginatedSpecification(request.pageNumber, request.pageSize);

            var productList = await _productRepository.ListAsync(getProductsPaginatedSpecification);

            var totalItems = await _productRepository.CountAsync();

            var productPaginitedList = new PaginiatedList<ProductResponse>(productList, totalItems, request.pageNumber, request.pageSize);

            return Result.Success(productPaginitedList);
        }
    }
}
