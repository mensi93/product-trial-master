
using Alten.ProductMaster.Application.Specifications.WishLists;
using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;

namespace Alten.ProductMaster.Application.WhishLists.AddToWhishList
{
    public class AddToWhishListCommandHandler : IRequestHandler<AddToWishlistCommand, Result<WishList>>
    {       
        private readonly IRepository<WishList> _wishlistRepository;

        public AddToWhishListCommandHandler(IRepository<WishList> wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;

        }
        public async Task<Result<WishList>> Handle(AddToWishlistCommand request)
        {
            var wishlistByUserIdSpecification = new WishlistByUserIdSpecification(request.UserId);
            var wishlist = await _wishlistRepository.FirstOrDefaultAsync(wishlistByUserIdSpecification);


            if (wishlist == null)
            {
                var wishlistResult = WishList.Create(request.UserId);
                if (wishlistResult.IsFailure)
                {
                    return Result.Failure<WishList>(wishlistResult.Error);
                }

                wishlist = wishlistResult.Value;
                await _wishlistRepository.AddAsync(wishlist);
            }

            var wishlistItemResult = WishlistItem.Create(request.ProductId);
            if (wishlistItemResult.IsFailure) return Result.Failure<WishList>(wishlistItemResult.Error);

            var addItemResult = wishlist.AddItem(wishlistItemResult.Value);
            if (addItemResult.IsFailure) return Result.Failure<WishList>(addItemResult.Error);

            await _wishlistRepository.UpdateAsync(wishlist);

            return Result.Success(wishlist);
        }
    }
}
