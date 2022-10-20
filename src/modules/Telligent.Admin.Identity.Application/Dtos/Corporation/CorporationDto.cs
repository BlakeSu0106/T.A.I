using Telligent.Consumer.Identity.Domain.Shared;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Identity.Application.Dtos.Corporation;

public class CorporationDto : EntityDto
{
    public string Name { get; set; }

    public string ShortName { get; set; }

    public EnterpriseType EnterpriseType { get; set; }
}