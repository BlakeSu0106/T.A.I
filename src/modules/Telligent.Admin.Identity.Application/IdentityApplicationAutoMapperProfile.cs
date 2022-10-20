using AutoMapper;
using AutoMapper.Execution;
using Telligent.Admin.Identity.Application.Dtos.Company;
using Telligent.Admin.Identity.Application.Dtos.Corporation;
using Telligent.Admin.Identity.Application.Dtos.Tenant;
using Telligent.Admin.Identity.Application.Dtos.AdminUser;
using Telligent.Admin.Identity.Domain.Organizations;
using Telligent.Admin.Identity.Domain.Users;

namespace Telligent.Admin.Identity.Application;

public class IdentityApplicationAutoMapperProfile : Profile
{
    public IdentityApplicationAutoMapperProfile()
    {
        ShouldMapProperty = prop =>
            prop.GetMethod is not null && (prop.GetMethod.IsAssembly || prop.GetMethod.IsPublic);

        CreateMap<Tenant, TenantDto>();
        CreateMap<Corporation, CorporationDto>();
        CreateMap<Company, CompanyDto>();

        CreateMap<CreateAdminUserDto, AdminUser>();
        CreateMap<AdminUser, AdminUserDto>();
    }
}