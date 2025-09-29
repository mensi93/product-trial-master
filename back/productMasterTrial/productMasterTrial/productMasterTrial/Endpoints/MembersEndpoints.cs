using Alten.ProductMasterTrial.Application.Common.Abstractions.RequestHandler;
using Alten.ProductMasterTrial.Application.Members.CreateMember;
using Alten.ProductMasterTrial.Application.Members.Login;
using Microsoft.AspNetCore.Mvc;

namespace productMasterTrial.Endpoints
{
    public static class MembersEndpoints
    {
        public static IEndpointRouteBuilder MapMembersEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/members")
               .WithTags("Members");

            group.MapPost("/account", async ([FromBody] CreateMemberCommand command, IRequestHandler<CreateMemberCommand, Result> handler) =>
            {
                var result = await handler.Handle(command);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result.Error);
            })
            .WithName("account")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);


            group.MapPost("/token",
                async ([FromBody] LoginCommand command, IRequestHandler<LoginCommand, Result<string>> handler) =>
                {
                    var result = await handler.Handle(command);

                    if (result.IsFailure)
                        return Results.BadRequest(new { error = result.Error });

                    return Results.Ok(new { token = result.Value });
                })
            .WithName("token")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

            return app;
        }

    }
}
