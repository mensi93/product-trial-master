namespace Alten.ProductMaster.Application.WhishLists.AddToWhishList
{
    public sealed record AddToWishlistCommand(
        Guid UserId,
        int ProductId
    );

}
