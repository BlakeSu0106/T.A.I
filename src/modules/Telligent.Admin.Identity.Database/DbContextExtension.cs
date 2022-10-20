using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telligent.Admin.Identity.Domain.Identities;
using Telligent.Core.Infrastructure.Database;

namespace Telligent.Admin.Identity.Database;

public static class DbContextExtension
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, string connection)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options
                .UseMySql(connection, ServerVersion.AutoDetect(connection))
                .UseOpenIddict<IdentityApplication, IdentityAuthorization, IdentityScope, IdentityToken, Guid>();
        });

        return services;
    }

    public static void RegisterDbContexts(this ContainerBuilder builder)
    {
        builder.RegisterType<IdentityDbContext>().As<BaseDbContext>().InstancePerLifetimeScope();
    }
}