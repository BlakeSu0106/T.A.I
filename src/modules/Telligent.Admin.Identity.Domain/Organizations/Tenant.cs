using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Consumer.Identity.Domain.Shared;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Identity.Domain.Organizations;

/// <summary>
/// 租戶
/// </summary>
[Table("tenant")]
public class Tenant : Entity
{
    /// <summary>
    /// 名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }

}