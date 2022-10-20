using Telligent.Consumer.Identity.Domain.Shared;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Identity.Application.Dtos.Tenant;

public class TenantDto : EntityDto
{
    public string Name { get; set; }

}