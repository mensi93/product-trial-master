using Alten.ProductMaster.Domain.ValueObjects;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMasterTrial.Domain.Entities
{
    public sealed class Product
    {
        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public string InternalReference { get; private set; }
        public int ShellId { get; private set; }
        public InventoryStatus InventoryStatus { get; private set; }
        public int Rating { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        private Product()
        {

        }
        private Product(string code,
            string name,
            string description,
            string image,
            decimal price,
            int quantity,
            string category,
            string internalReference,
            int shellId,
            InventoryStatus inventoryStatus)
        {
            Code = code;
            Name = name;
            Description = description;
            Image = image;
            Price = price;
            Quantity = quantity;
            Category = category;
            InternalReference = internalReference;
            ShellId = shellId;
            InventoryStatus = inventoryStatus;
            Rating = 0;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = null;
        }

        public static Result<Product> Create(string code,
            string name,
            string description,
            string image,
            decimal price,
            int quantity,
            string category,
            string internalReference,
            int shellId)
        {

            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<Product>(DomainErrors.Product.EmptyCode);

            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<Product>(DomainErrors.Product.EmptyName);

            if (price < 0)
                return Result.Failure<Product>(DomainErrors.Product.PriceIsNegative);

            if (quantity < 0)
                return Result.Failure<Product>(DomainErrors.Product.QuantityIsNegative);

            if (string.IsNullOrWhiteSpace(image))
                return Result.Failure<Product>(DomainErrors.Product.EmptyImage);


            if (string.IsNullOrWhiteSpace(category))
                return Result.Failure<Product>(DomainErrors.Product.EmptyCategory);

            if (string.IsNullOrWhiteSpace(internalReference))
                return Result.Failure<Product>(DomainErrors.Product.EmptyInternalReference);

            if (shellId < 0)
                return Result.Failure<Product>(DomainErrors.Product.ShellIdIsNegative);

            var inventoryStatus = InventoryStatus.Create(quantity);

            if (inventoryStatus.IsFailure)
                return Result.Failure<Product>(inventoryStatus.Error);


            return new Product(
             code,
             name,
             description,
             image,
             price,
             quantity,
             category,
             internalReference,
             shellId,
             inventoryStatus.Value);
        }


        public Result Update(
           string name
         , string description
         , string image
         , decimal price
         , int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure(DomainErrors.Product.EmptyName);
            if (price < 0)
                return Result.Failure(DomainErrors.Product.PriceIsNegative);
            if (quantity < 0)
                return Result.Failure(DomainErrors.Product.QuantityIsNegative);

            var inventoryStatus = InventoryStatus.Create(quantity);

            if (inventoryStatus.IsFailure)
                return Result.Failure<Product>(inventoryStatus.Error);


            Name = name;
            Description = description;
            Image = image;
            Price = price;
            Quantity = quantity;
            UpdatedAt = DateTime.UtcNow;
            InventoryStatus = inventoryStatus.Value;

            return Result.Success();
        }
    }
}
