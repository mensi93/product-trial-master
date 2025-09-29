namespace Alten.ProductMaster.Application.Carts.AddToCart
{
    public sealed record AddToCartCommand(
        Guid UserId,
        int ProductId,
        int Quantity = 1
    );

}
