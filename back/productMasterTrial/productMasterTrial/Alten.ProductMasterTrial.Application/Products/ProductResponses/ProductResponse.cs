using Alten.ProductMasterTrial.Domain.ValueObjects;

namespace Alten.ProductMaster.Application.Products.ProductResponses
{
    public sealed record ProductResponse
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int ShellId { get; set; }
        public InventoryStatusEnum InventoryStatus { get; set; }
        public int Rating { get; set; }
    }

}
