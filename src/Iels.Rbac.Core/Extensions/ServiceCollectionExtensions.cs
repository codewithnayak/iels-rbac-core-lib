using Iels.Rbac.Core.Authorization;
using Iels.Rbac.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Iels.Rbac.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIelsRbac<TDbContext>(
        this IServiceCollection services)
        where TDbContext : DbContext
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserContext, CurrentUserContext<TDbContext>>();
        services.AddSingleton<IAuthorizationHandler, IelsAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("IELS", policy =>
                policy.Requirements.Add(new IelsRequirement()));
        });

        return services;
    }

    public static IServiceCollection AddIelsPermissionPolicy(
        this IServiceCollection services,
        string policyName,
        string permission)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(policyName, policy =>
                policy.Requirements.Add(new IelsRequirement(permission)));
        });

        return services;
    }
}