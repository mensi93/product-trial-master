using Alten.ProductMaster.SharedKirnel;
using Alten.ProductMasterTrial.Domain.Errors;
using Alten.ProductMasterTrial.Domain.ValueObjects;

namespace Alten.ProductMaster.Domain.ValueObjects
{
    public sealed class InventoryStatus : ValueObject
    {
        public const int LowStock = 10;

        public const int OutOfStock = 0;

        public InventoryStatusEnum Value { get; private set; }
        private InventoryStatus()
        {
            
        }
        private InventoryStatus(InventoryStatusEnum inventoryStatusEnum)
        {
            Value = inventoryStatusEnum;
        }

        public static Result<InventoryStatus> Create(int quantity)
        {
            if (quantity < 0)
                return Result.Failure<InventoryStatus>(DomainErrors.Product.QuantityIsNegative);

            InventoryStatusEnum status = quantity switch
            {
                > 10 => InventoryStatusEnum.InStock,
                > 0 => InventoryStatusEnum.LowStock,
                0 => InventoryStatusEnum.OutOfStock
            };

            return Result.Success(new InventoryStatus(status));
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
