using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class UserGroupType
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string TypeName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Description { get; set; }

    public int MembersMin { get; set; } = 1;

    public int MembersMax { get; set; } = 5;

    public int MaxWorkspaces { get; set; } = 1;

    public int MaxProjectsPerWorkspace { get; set; } = 3;

    [Column(TypeName = "bigint")]
    public long MaxStorageBytes { get; set; } = 1_000_000_000; // 1GB

    public bool AllowCustomBranding { get; set; } = false;

    public bool AllowAdvancedFeatures { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public int SortOrder { get; set; } = 0;

    [Column(TypeName = "jsonb")]
    public string? Features { get; set; }

    [Column(TypeName = "jsonb")]
    public string? Limitations { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<Plan> Plans { get; set; } = new List<Plan>();
}
