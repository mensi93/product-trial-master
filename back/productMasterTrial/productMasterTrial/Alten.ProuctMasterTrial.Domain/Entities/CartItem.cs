using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Domain.Entities
{
    public sealed class CartItem
    {
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }


        private CartItem()
        {
            
        }
        private CartItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public static Result<CartItem> Create(int productId, int quantity)
        {
            if (productId == 0)
                return Result.Failure<CartItem>(DomainErrors.CartItemErrors.EmptyProduct);

            if (quantity <= 0)
                return Result.Failure<CartItem>(DomainErrors.CartItemErrors.NegativeQuantity);

            return Result.Success(new CartItem(productId, quantity));
        }

        public void IncreaseQuantity(int amount)
        {
            if (amount <= 0) return;
            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            if (amount <= 0) return;
            Quantity -= amount;
            if (Quantity < 0) Quantity = 0;
        }

    }
}
