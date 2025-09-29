using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Products
{
    public class ProductByIdsSpecification : Specification<Product>
    {
        public ProductByIdsSpecification(IEnumerable<int> productIds)
        {
            Query.Where(p => productIds.Contains(p.Id));
        }
    }
}
