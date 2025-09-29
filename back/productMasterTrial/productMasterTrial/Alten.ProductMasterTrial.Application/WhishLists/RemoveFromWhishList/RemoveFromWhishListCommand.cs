namespace Alten.ProductMaster.Application.WhishLists.RemoveFromWhishList
{
    public sealed record RemoveFromWhishListCommand(
        Guid UserId,
        int ProductId
    );
}
