using Alten.ProductMaster.Application.Specifications.Carts;
using Alten.ProductMaster.Application.Specifications.Products;
using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Domain.Entities;
using Alten.ProductMasterTrial.Domain.Errors;

namespace Alten.ProductMaster.Application.Carts.AddToCart
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, Result<Cart>>
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Member> _MemberRepository;

        public AddToCartCommandHandler(IRepository<Cart> cartRepository, IRepository<Product> productRepository, IRepository<Member> memberRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _MemberRepository = memberRepository;
        }
        public async Task<Result<Cart>> Handle(AddToCartCommand request)
        {



            var member = await _MemberRepository.GetByIdAsync(request.UserId);

            if(member == null)
            {
                return Result.Failure<Cart>(DomainErrors.CartErrors.MemberNotFound);
            }

            var cartByUserIdSpecification = new CartByUserIdSpecification(request.UserId);

            var cart = await _cartRepository.FirstOrDefaultAsync(cartByUserIdSpecification);

            if (cart == null)
            {
                var cartResult = Cart.Create(request.UserId);

                if (cartResult.IsFailure)
                {
                    return Result.Failure<Cart>(cartResult.Error);
                }

                cart = cartResult.Value;

                await _cartRepository.AddAsync(cart);
            }
            var cartItemResult = CartItem.Create(request.ProductId, request.Quantity);

            if (cartItemResult.IsFailure)
            {
                return Result.Failure<Cart>(cartItemResult.Error);
            }

            var product =await  _productRepository.GetByIdAsync(request.ProductId);

            if(product == null)
            {
                return Result.Failure<Cart>(DomainErrors.CartErrors.ProductNotFound);

            }

            var addItemResult = cart.AddItem(cartItemResult.Value);

            if (addItemResult.IsFailure)
            {
                return Result.Failure<Cart>(addItemResult.Error);
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
