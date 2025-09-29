using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMasterTrial.Application.Specifications.Products
{
    public class ProductByCodeSpecification : Specification<Product>
    {
        public ProductByCodeSpecification(string code)
        {
            Query.Where(p => p.Code == code);
        }
    }
}
