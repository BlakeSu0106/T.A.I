using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Identity.Domain.Users;

[Table("admin_user")]
public class AdminUser : Entity
{
    [Column("user_id")] public string UserId { get; set; }

    [Column("password")] public string Password { get; set; }

    [Column("email")] public string Email { get; set; }

    [Column("name")] public string Name { get; set; }
}