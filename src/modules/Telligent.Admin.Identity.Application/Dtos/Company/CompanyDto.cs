using Telligent.Admin.Identity.Application.Dtos.Corporation;
using Telligent.Admin.Identity.Application.Dtos.Tenant;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Identity.Application.Dtos.Company;

public class CompanyDto : EntityDto
{
    public TenantDto Tenant { get; set; }

    public CorporationDto Corporation { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}