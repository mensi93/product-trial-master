using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Domain.Entities
{
    public sealed class WishlistItem
    {
        public int ProductId { get; private set; }

        private WishlistItem()
        {
            
        }
        private WishlistItem(int productId)
        {
            ProductId = productId;
        }

        public static Result<WishlistItem> Create(int productId)
        {
            if (productId == 0)
                return Result.Failure<WishlistItem>(DomainErrors.WishlistItems.EmptyProductId);

            return Result.Success(new WishlistItem(productId));
        }
    }
}
