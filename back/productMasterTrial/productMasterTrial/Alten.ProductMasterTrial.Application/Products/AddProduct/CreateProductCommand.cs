using Alten.ProductMasterTrial.Domain.ValueObjects;

namespace Alten.ProductMasterTrial.Application.Products.AddProduct
{
    public sealed record CreateProductCommand(
          string code
        , string name
        , string description
        , string image
        , string category
        , decimal price
        , int quantity
        , string internalReference
        , int shellId
        , int rating
        );
}

