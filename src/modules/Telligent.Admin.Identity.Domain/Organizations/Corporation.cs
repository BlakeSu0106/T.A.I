using System.ComponentModel.DataAnnotations.Schema;
using Telligent.Consumer.Identity.Domain.Shared;
using Telligent.Core.Domain.Entities;

namespace Telligent.Admin.Identity.Domain.Organizations;

[Table("corporation")]
public class Corporation : Entity
{
    /// <summary>
    /// 集團名稱
    /// </summary>
    [Column("name")]
    public string Name { get; set; }


    /// <summary>
    /// 集團簡寫
    /// </summary>
    [Column("short_name")]
    public string ShortName { get; set; }

    /// <summary>
    /// 企業類型
    /// </summary>
    [Column("enterprise_type")]
    public EnterpriseType EnterpriseType { get; set; }
}