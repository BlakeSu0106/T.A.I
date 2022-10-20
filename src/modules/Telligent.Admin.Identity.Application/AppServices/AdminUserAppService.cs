using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Telligent.Admin.Identity.Application.Dtos.AdminUser;
using Telligent.Admin.Identity.Domain.Users;
using Telligent.Admin.Identity.Application;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Core.Infrastructure.Encryption;
using Telligent.Core.Infrastructure.Generators;
using Microsoft.Extensions.Configuration;

namespace Telligent.Admin.Application.AppServices;

public class AdminUserAppService : CrudAppService<AdminUser,AdminUserDto,CreateAdminUserDto,AdminUserDto>
{

    private readonly IConfiguration _configuration;

    public AdminUserAppService(
        IRepository<AdminUser> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        UnitOfWork uow)
        : base(repository, mapper, httpContextAccessor)
    {
        _configuration = configuration;

        if (httpContextAccessor.HttpContext == null) return;

    }

    /// <summary>
    /// 建立管理者帳號
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public override async Task<AdminUserDto> CreateAsync(CreateAdminUserDto dto)
    {

        var AdminDto = await GetAsync(m => m.UserId.Equals(dto.UserId)
                                           || m.Email.Equals(dto.Email));

        if (AdminDto != null) throw new ValidationException("帳號已重複");

        var salt = _configuration.GetSection("Salt").Value;

        var encryptPassword = EncryptionHelper.EncryptArgon2(dto.Password, System.Text.Encoding.Default.GetBytes(salt));

        var adminEntity = Mapper.Map<AdminUser>(dto);

        adminEntity.Id = SequentialGuidGenerator.Instance.GetGuid();
        adminEntity.Password = encryptPassword;
        adminEntity.CreatorId = adminEntity.Id;

        await Repository.CreateAsync(adminEntity);
        await Repository.SaveChangesAsync();

        return await GetAsync(adminEntity.Id);
    }
}
