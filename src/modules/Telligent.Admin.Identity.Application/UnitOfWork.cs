using Telligent.Admin.Identity.Domain.Organizations;
using Telligent.Admin.Identity.Domain.Users;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Database;

namespace Telligent.Admin.Identity.Application;

public class UnitOfWork : IDisposable
{
    private bool _disposed;

    public UnitOfWork(
        BaseDbContext context,
        IRepository<Tenant> tenantRepository,
        IRepository<Company> companyRepository,
        IRepository<AdminUser> adminUserRepository)
    {
        Context = context;
        TenantRepository = tenantRepository;
        CompanyRepository = companyRepository;
        AdminUserRepository = adminUserRepository;
    }

    public IRepository<Tenant> TenantRepository { get; }
    public IRepository<Company> CompanyRepository { get; }

    public IRepository<AdminUser> AdminUserRepository { get; }

    /// <summary>
    /// Context
    /// </summary>
    public BaseDbContext Context { get; private set; }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// SaveChange
    /// </summary>
    /// <returns></returns>
    public async Task<int> SaveChangeAsync()
    {
        return await Context.SaveChangesAsync();
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
            {
                Context.Dispose();
                Context = null;
            }

        _disposed = true;
    }
}