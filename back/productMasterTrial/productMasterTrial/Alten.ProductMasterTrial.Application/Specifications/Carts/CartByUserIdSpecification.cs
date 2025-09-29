using Alten.ProductMaster.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.Carts
{
    public class CartByUserIdSpecification : Specification<Cart>
    {
        public CartByUserIdSpecification(Guid userId)
        {
            Query.Where(c => c.UserId == userId).Include(c => c.Items);
        }
    }
}
