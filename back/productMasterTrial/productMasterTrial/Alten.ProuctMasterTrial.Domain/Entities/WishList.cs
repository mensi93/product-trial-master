using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Domain.Entities
{
    public sealed class WishList
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        private readonly List<WishlistItem> _items = new();
        public IReadOnlyCollection<WishlistItem> Items => _items.AsReadOnly();

        private WishList()
        {
            
        }
        private WishList(Guid id, Guid userId, List<WishlistItem>? items = null)
        {
            Id = id;
            UserId = userId;
            if (items != null)
                _items.AddRange(items);
        }

        public static Result<WishList> Create(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return Result.Failure<WishList>(DomainErrors.WhishLists.EmptyUserId);
            }

            return Result.Success(new WishList(Guid.NewGuid(), userId));
        }

        public Result AddItem(WishlistItem item)
        {
            if (item == null)
            {
                return Result.Failure(DomainErrors.WhishLists.WishlistItemEmpty);
            }

            if (_items.Any(i => i.ProductId == item.ProductId))
            {
                return Result.Failure(DomainErrors.WhishLists.WishlistItemAlreadyExist);
            }

            _items.Add(item);
            return Result.Success();
        }

        public Result RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                return Result.Failure(DomainErrors.WhishLists.ProductNotFound);
            }

            _items.Remove(item);
            return Result.Success();
        }

    }
}
