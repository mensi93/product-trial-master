using Alten.ProductMaster.Application.WhishLists.AddToWhishList;
using Alten.ProductMaster.Application.WhishLists.RemoveFromWhishList;
using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Microsoft.AspNetCore.Authorization;

namespace productMasterTrial.Endpoints
{
    public static class WishListEndpoints
    {
        public static IEndpointRouteBuilder MapWishListEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/wishlist")
               .WithTags("WishLists");


            group.MapPost("/add",
                [Authorize] async (AddToWishlistCommand command, IRequestHandler<AddToWishlistCommand, Result<WishList>> handler) =>
                {
                    var result = await handler.Handle(command);

                    if (result.IsFailure)
                        return Results.BadRequest(new { error = result.Error });

                    return Results.Ok(result.Value);
                })
            .WithName("AddToWishlist")
            .Produces<WishList>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            group.MapPost("/remove",
                [Authorize] async (RemoveFromWhishListCommand command, IRequestHandler<RemoveFromWhishListCommand, Result<WishList>> handler) =>
                {
                    var result = await handler.Handle(command);

                    if (result.IsFailure)
                        return Results.BadRequest(new { error = result.Error });

                    return Results.Ok(result.Value);
                })
            .WithName("RemoveFromWishlist")
            .Produces<WishList>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            return app;
        }

    }
}
