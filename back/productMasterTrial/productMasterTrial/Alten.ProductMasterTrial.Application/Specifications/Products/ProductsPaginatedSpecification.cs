using Alten.ProductMaster.Application.Products.ProductResponses;
using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Products
{
    public class ProductsPaginatedSpecification : Specification<Product,ProductResponse>
    {
        public ProductsPaginatedSpecification(int pageNumber, int pageSize)
        {
            Query.AsNoTracking()
                 .OrderBy(p => p.Id)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .Select(p=>new ProductResponse
                 {
                     Code = p.Code,
                     Category = p.Category,
                     Description = p.Description,
                     Image = p.Image,   
                     Name = p.Name,
                     Price = p.Price,
                     Quantity = p.Quantity,
                     InventoryStatus = p.InventoryStatus.Value,
                     Rating = p.Rating,
                     ShellId = p.ShellId
                 });
        }
    }
}
