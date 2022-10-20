using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Identity.Application.Dtos.AdminUser;

public class AdminUserDto : EntityDto
{
    public string UserId { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }
}
