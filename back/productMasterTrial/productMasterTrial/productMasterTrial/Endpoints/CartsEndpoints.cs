using Alten.ProductMaster.Application.Carts.AddToCart;
using Alten.ProductMaster.Application.Carts.RemoveFromCart;
using Alten.ProductMaster.Domain.Entities;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Microsoft.AspNetCore.Authorization;

namespace productMasterTrial.Endpoints
{
    public static class CartsEndpoints
    {
        public static IEndpointRouteBuilder MapCartsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/carts")
               .WithTags("Carts");

            group.MapPost("/add",
                [Authorize] async (AddToCartCommand command, IRequestHandler<AddToCartCommand, Result<Cart>> handler) =>
                {
                    var result = await handler.Handle(command);

                    if (result.IsFailure)
                        return Results.BadRequest(new { error = result.Error });

                    return Results.Ok(result.Value);
                })
            .WithName("AddToCart")
            .Produces<Cart>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);


            group.MapPost("/remove",
                [Authorize] async (RemoveFromCartCommand command, IRequestHandler<RemoveFromCartCommand, Result<Cart>> handler) =>
                {
                    var result = await handler.Handle(command);

                    if (result.IsFailure)
                        return Results.BadRequest(new { error = result.Error });

                    return Results.Ok(result.Value);
                })
            .WithName("RemoveFromCart")
            .Produces<Cart>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            return app;
        }

    }
}
