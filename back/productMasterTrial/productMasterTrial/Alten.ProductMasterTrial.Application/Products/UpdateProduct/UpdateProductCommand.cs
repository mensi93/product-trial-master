namespace Alten.ProductMasterTrial.Application.Products.UpdateProduct
{
    public sealed record UpdateProductCommand
        (
          string code
         ,string name
         ,string description
         ,string image
         ,decimal price
         ,int quantity
        );

}
