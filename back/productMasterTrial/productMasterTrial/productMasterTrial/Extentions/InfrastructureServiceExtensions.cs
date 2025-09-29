using Alten.ProductMaster.Application.Common.Authentication;
using Alten.ProductMaster.Infrastructure.Authentication;
using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Alten.ProductMasterTrial.Infrastructure.Persistance;
using Alten.ProductMasterTrial.Infrastructure.Persistance.Repository;
using Microsoft.EntityFrameworkCore;
using productMasterTrial.Attributes;

namespace productMasterTrial.Extentions;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ProductTrialDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.Configure<AdminOptions>(config.GetSection("AdminOptions"));

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfReadRepository<>));

        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<AdminOnlyEndpointFilter>();

        return services;
    }
}
