using Microsoft.EntityFrameworkCore;
using Telligent.Admin.Identity.Domain.Identities;
using Telligent.Admin.Identity.Domain.Organizations;
using Telligent.Admin.Identity.Domain.Users;
using Telligent.Core.Infrastructure.Database;

namespace Telligent.Admin.Identity.Database;

public class IdentityDbContext : BaseDbContext
{
    public IdentityDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<Corporation> Corporations { get; set; }

    public DbSet<Company> Company { get; set; }

    public DbSet<AdminUser> AdminsUser { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region identity

        builder.Entity<IdentityApplication>().ToTable("identity_application");
        builder.Entity<IdentityAuthorization>().ToTable("identity_authorization");
        builder.Entity<IdentityScope>().ToTable("identity_scope");
        builder.Entity<IdentityToken>().ToTable("identity_token");

        builder.Entity<IdentityAuthorization>().Property("ApplicationId").HasColumnName("application_id");
        builder.Entity<IdentityToken>().Property("ApplicationId").HasColumnName("application_id");
        builder.Entity<IdentityToken>().Property("AuthorizationId").HasColumnName("authorization_id");

        #endregion

        builder.Entity<Tenant>().Ignore(t => t.TenantId);
        builder.Entity<AdminUser>().Ignore(t => t.TenantId);

        base.OnModelCreating(builder);
    }
}