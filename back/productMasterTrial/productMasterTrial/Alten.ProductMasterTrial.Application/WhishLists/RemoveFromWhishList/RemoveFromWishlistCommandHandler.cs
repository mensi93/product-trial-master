using Alten.ProductMaster.Application.Specifications.WishLists;
using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Application.WhishLists.RemoveFromWhishList
{
    public class RemoveFromWishlistCommandHandler : IRequestHandler<RemoveFromWhishListCommand, Result<WishList>>

    {
        private readonly IRepository<WishList> _wishlistRepository;

        public RemoveFromWishlistCommandHandler(IRepository<WishList> wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }
        public async Task<Result<WishList>> Handle(RemoveFromWhishListCommand request)
        {
            var wishlistByUserIdSpecification = new WishlistByUserIdSpecification(request.UserId);
            var wishlist = await _wishlistRepository.FirstOrDefaultAsync(wishlistByUserIdSpecification);

            if (wishlist == null)
            {
                return Result.Failure<WishList>(DomainErrors.WhishLists.WishListNotFound);
            }

            var removeResult = wishlist.RemoveItem(request.ProductId);

            if (removeResult.IsFailure)
            {
                return Result.Failure<WishList>(removeResult.Error);
            }

            await _wishlistRepository.UpdateAsync(wishlist);

            return Result.Success(wishlist);
        }
    }
}
