using Alten.ProductMaster.Application.Carts.AddToCart;
using Alten.ProductMaster.Application.Carts.RemoveFromCart;
using Alten.ProductMaster.Application.WhishLists.AddToWhishList;
using Alten.ProductMaster.Application.WhishLists.RemoveFromWhishList;
using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Members.CreateMember;
using Alten.ProductMasterTrial.Application.Members.Login;
using Alten.ProductMasterTrial.Application.Products.AddProduct;
using Alten.ProductMasterTrial.Application.Products.DeleteProduct;
using Alten.ProductMasterTrial.Application.Products.GetProductByCode;
using Alten.ProductMasterTrial.Application.Products.UpdateProduct;
using Alten.ProductMaster.Application.Products.GetProductList;
using Alten.ProductMaster.Application.Products.ProductResponses;
using Alten.ProductMaster.Application.Common.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace productMasterTrial.Extentions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AddToCartCommand, Result<Cart>>, AddToCartCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveFromCartCommand, Result<Cart>>, RemoveFromCartCommandHandler>();

        services.AddScoped<IRequestHandler<CreateMemberCommand, Result>, CreateMemberCommandHandler>();
        services.AddScoped<IRequestHandler<LoginCommand, Result<string>>, LoginCommandHandler>();

        services.AddScoped<IRequestHandler<CreateProductCommand, Result>, CreateProductCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteProductCommand, Result>, DeleteProductCommandHandler>();
        services.AddScoped<IRequestHandler<GetProductByCodeQuery, Result<ProductResponse>>, GetProductByCodeQueryHandler>();
        services.AddScoped<IRequestHandler<GetProductListQuery, Result<PaginiatedList<ProductResponse>>>, GetProductListQueryHandler>();
        services.AddScoped<IRequestHandler<UpdateProductCommand, Result>, UpdateProductCommandHandler>();

        services.AddScoped<IRequestHandler<AddToWishlistCommand, Result<WishList>>, AddToWhishListCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveFromWhishListCommand, Result<WishList>>, RemoveFromWishlistCommandHandler>();

        return services;
    }
}
