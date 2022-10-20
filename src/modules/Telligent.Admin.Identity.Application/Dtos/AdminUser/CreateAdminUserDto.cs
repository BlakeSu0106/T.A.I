using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Identity.Application.Dtos.AdminUser;

public class CreateAdminUserDto : EntityDto
{
    internal new Guid Id { get; set; }

    public string UserId { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }
}