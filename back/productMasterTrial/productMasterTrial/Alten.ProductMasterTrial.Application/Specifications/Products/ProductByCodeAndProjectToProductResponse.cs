using Alten.ProductMaster.Application.Products.ProductResponses;
using Alten.ProductMaster.Domain.ValueObjects;
using Alten.ProductMasterTrial.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMasterTrial.Application.Specifications.Products
{
    public class ProductByCodeAndProjectToProductResponse : Specification<Product, ProductResponse>
    {
        public ProductByCodeAndProjectToProductResponse(string code)
        {
            Query.AsNoTracking()
                 .Where(p => p.Code == code)
                 .Select(p => new ProductResponse
                 {
                     Code = p.Code,
                     Name = p.Name,
                     Description = p.Description,
                     Image = p.Image,
                     Category = p.Category,
                     Price = p.Price,
                     Quantity = p.Quantity,
                     ShellId = p.ShellId,
                     InventoryStatus = InventoryStatus.Create(p.Quantity).Value.Value,
                     Rating = p.Rating,

                 });
        }
    }
}
