using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Domain.Entities
{
    public sealed class Cart
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        private readonly List<CartItem> _items = new();
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        public decimal Total { get; private set; }

        private Cart()
        {
            
        }
        private Cart(Guid id, Guid userId, List<CartItem>? items = null)
        {
            Id = id;
            UserId = userId;
            if (items != null)
                _items.AddRange(items);
        }

        public static Result<Cart> Create(Guid userId)
        {
            if (userId == Guid.Empty)
                return Result.Failure<Cart>(DomainErrors.CartErrors.EmptyUserId);

            return Result.Success(new Cart(Guid.NewGuid(), userId));
        }

        public Result AddItem(CartItem item)
        {
            if (item == null)
                return Result.Failure(DomainErrors.CartErrors.EmptyItem);

            var existingItem = _items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(item.Quantity);
            }
            else
            {
                _items.Add(item);
            }

            return Result.Success();
        }

        public Result RemoveItem(int productId, int quantity = 1)
        {
            var existingItem = _items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem == null)
                return Result.Failure(DomainErrors.CartErrors.ItemNotFound);

            existingItem.DecreaseQuantity(quantity);
            if (existingItem.Quantity == 0)
                _items.Remove(existingItem);

            return Result.Success();
        }

        public void Clear() => _items.Clear();

        public decimal CalculateTotal(Func<int, decimal> getPrice)
        {
            if (getPrice == null)
                throw new ArgumentNullException(nameof(getPrice));

            Total = _items.Sum(i => i.Quantity * getPrice(i.ProductId));

            return Total;
        }
    }
}
