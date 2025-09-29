using Alten.ProductMaster.Application.Common.Pagination;
using Alten.ProductMaster.Application.Products.GetProductList;
using Alten.ProductMaster.Application.Products.ProductResponses;
using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Products.AddProduct;
using Alten.ProductMasterTrial.Application.Products.DeleteProduct;
using Alten.ProductMasterTrial.Application.Products.GetProductByCode;
using Alten.ProductMasterTrial.Application.Products.UpdateProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using productMasterTrial.Attributes;

namespace productMasterTrial.Endpoints
{
    public static class ProductsEndpoints
    {
        public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/products")
                           .WithTags("Products");

            group.MapPost("/", [Authorize] async ([FromBody] CreateProductCommand command, IRequestHandler<CreateProductCommand, Result> handler) =>
            {
                var result = await handler.Handle(command);

                if (result.IsFailure)
                    return Results.BadRequest(new { error = result.Error });

                return Results.Ok(new { message = "Product created successfully" });
            })
            .AddEndpointFilter<AdminOnlyEndpointFilter>()
            .WithName("CreateProduct")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);


            group.MapGet("/{code}",
                [Authorize] async ([FromQuery] string code, IRequestHandler<GetProductByCodeQuery, Result<ProductResponse>> handler) =>
                {
                    var query = new GetProductByCodeQuery(code);
                    var result = await handler.Handle(query);
                    return result.IsFailure ? Results.NotFound(new { error = result.Error }) : Results.Ok(result.Value);
                })
            .WithName("GetProductByCode")
            .Produces<ProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);


            group.MapGet("/",
               [Authorize] async (int pageNumber, int pageSize, IRequestHandler<GetProductListQuery, Result<PaginiatedList<ProductResponse>>> handler) =>
               {
                   var query = new GetProductListQuery(pageNumber, pageSize);
                   var result = await handler.Handle(query);

                   if (result.IsFailure)
                       return Results.BadRequest(new { error = result.Error });

                   return Results.Ok(result.Value);
               })
            .WithName("GetProductList")
            .Produces<PaginiatedList<ProductResponse>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);


            group.MapPut("/{code}",
                 [Authorize] async (string code, UpdateProductCommand command, IRequestHandler<UpdateProductCommand, Result> handler) =>
                 {
                     // Assurer que le code de l'URL correspond au code du body
                     if (code != command.code)
                         return Results.BadRequest(new { error = "Code in URL and body must match" });

                     var result = await handler.Handle(command);

                     if (result.IsFailure)
                         return Results.BadRequest(new { error = result.Error });

                     return Results.Ok();
                 })
            .AddEndpointFilter<AdminOnlyEndpointFilter>()
            .WithName("UpdateProduct")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);


            group.MapDelete("/{code}",
                 [Authorize] async (string code, IRequestHandler<DeleteProductCommand, Result> handler) =>
                 {
                     var command = new DeleteProductCommand(code);
                     var result = await handler.Handle(command);

                     if (result.IsFailure)
                         return Results.BadRequest(new { error = result.Error });

                     return Results.Ok(new { message = "Product deleted successfully" });
                 })
            .AddEndpointFilter<AdminOnlyEndpointFilter>()
            .WithName("DeleteProduct")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);



            return app;
        }

    }
}
