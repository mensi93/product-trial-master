using Alten.ProductMaster.Application.Specifications.Carts;
using Alten.ProductMaster.Application.Specifications.Products;
using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Application.Carts.RemoveFromCart
{
    public class RemoveFromCartCommandHandler : IRequestHandler<RemoveFromCartCommand, Result<Cart>>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;

        public RemoveFromCartCommandHandler(IRepository<Cart> cartRepository, IRepository<Product> productRepository, IRepository<Member> memberRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public async Task<Result<Cart>> Handle(RemoveFromCartCommand request)
        {
            var cartByUserIdSpecification = new CartByUserIdSpecification(request.UserId);

            var cart = await _cartRepository.FirstOrDefaultAsync(cartByUserIdSpecification);

            if (cart == null)
            {
                return Result.Failure<Cart>(DomainErrors.CartErrors.CartNotFound);
            }

            var removeResult = cart.RemoveItem(request.ProductId);

            if (removeResult.IsFailure)
            {
                return Result.Failure<Cart>(removeResult.Error);
            }

            await _cartRepository.UpdateAsync(cart);

            var productIds = cart.Items.Select(i => i.ProductId).ToList();

            var productByIdsSpecification = new ProductByIdsSpecification(productIds);

            var products = await _productRepository.ListAsync(productByIdsSpecification);

            decimal getPrice(int productId) => products.First(p => p.Id == productId).Price;

            decimal total = cart.CalculateTotal(getPrice);



            return Result.Success(cart);
        }
    }
}
