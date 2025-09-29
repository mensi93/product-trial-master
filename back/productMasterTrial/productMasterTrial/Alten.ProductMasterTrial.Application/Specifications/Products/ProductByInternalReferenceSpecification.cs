using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Products
{
    public class ProductByInternalReferenceSpecification : Specification<Product>
    {
        public ProductByInternalReferenceSpecification(string internalReference)
        {
            Query.Where(p => p.InternalReference == internalReference);
        }
    }
}
