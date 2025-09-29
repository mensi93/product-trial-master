using Alten.ProductMaster.Domain.Entities;
using Ardalis.Specification;

namespace Alten.ProductMaster.Application.Specifications.WishLists
{
    public class WishlistByUserIdSpecification : Specification<WishList>
    {
        public WishlistByUserIdSpecification(Guid userId)
        {
            Query.Where(w => w.UserId == userId).Include(w=>w.Items);
        }
    }
}
