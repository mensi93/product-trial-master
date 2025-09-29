
using Alten.ProductMaster.Application.Common.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace productMasterTrial.Attributes
{
    public class AdminOnlyEndpointFilter : IEndpointFilter
    {
        private readonly AdminOptions _adminOptions;

        public AdminOnlyEndpointFilter(IOptions<AdminOptions> options)
        {
            _adminOptions = options.Value;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                return TypedResults.Unauthorized();
            }

            var email = user.FindFirstValue(ClaimTypes.Email);

            if (email == null || !_adminOptions.Emails.Contains(email, StringComparer.OrdinalIgnoreCase))
            {
                return TypedResults.Forbid();
            }

            return await next(context);
        }
    }
}
