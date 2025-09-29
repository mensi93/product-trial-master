namespace Alten.ProductMaster.Application.Carts.RemoveFromCart
{
    public sealed record RemoveFromCartCommand(
        Guid UserId,
        int ProductId
    );
}
