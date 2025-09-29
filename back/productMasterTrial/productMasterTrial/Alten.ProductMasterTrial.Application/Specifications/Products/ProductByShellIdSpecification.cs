using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Products
{
    public class ProductByShellIdSpecification : Specification<Product>
    {
        public ProductByShellIdSpecification(int shellId)
        {
            Query.Where(p => p.ShellId == shellId);
        }
    }
}
