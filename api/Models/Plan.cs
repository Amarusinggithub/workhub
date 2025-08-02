using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class Plan
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string PlanName { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? PlanDescription { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentMonthlyPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? CurrentYearlyPrice { get; set; }

    public bool IsOfferAvailable { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public bool IsPopular { get; set; } = false;

    public int MaxUsers { get; set; }
    public int MaxProjects { get; set; }
    public int MaxWorkspaces { get; set; }
    public long MaxStorageGB { get; set; }

    public bool HasCustomBranding { get; set; } = false;
    public bool HasPrioritySupport { get; set; } = false;
    public bool HasAdvancedAnalytics { get; set; } = false;
    public bool HasAPIAccess { get; set; } = false;

    [Column(TypeName = "jsonb")]
    public string? Features { get; set; }

    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [Required]
    public int UserGroupTypeId { get; set; }
    public UserGroupType UserGroupType { get; set; }

    [Required]
    public int SoftwareId { get; set; }
    public Software Software { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<Include> Includes { get; set; } = new List<Include>();
    public ICollection<Prerequisite> Prerequisites { get; set; } = new List<Prerequisite>();
    public ICollection<OptionIncluded> OptionIncludes { get; set; } = new List<OptionIncluded>();
    public ICollection<PlanHistory> PlanHistories { get; set; } = new List<PlanHistory>();
}
