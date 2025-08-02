using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;

namespace api.Models;


public class Software
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string SoftwareName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? ShortDescription { get; set; }

    [MaxLength(2000)]
    public string? Details { get; set; }

    [MaxLength(500)]
    public string? AccessLink { get; set; }

    [MaxLength(500)]
    public string? LogoUrl { get; set; }

    [MaxLength(500)]
    public string? BannerImageUrl { get; set; }

    [MaxLength(2000)]
    public string? ScreenshotUrls { get; set; }

    [MaxLength(100)]
    public string? Version { get; set; }

    public SoftwareType Type { get; set; } = SoftwareType.WebApp;

    public SoftwareStatus Status { get; set; } = SoftwareStatus.Active;

    public bool IsPublic { get; set; } = true;

    public bool IsFeatured { get; set; } = false;

    public bool HasFreeTier { get; set; } = false;

    public bool HasFreeTrial { get; set; } = false;

    public int FreeTrialDays { get; set; } = 0;

    [MaxLength(50)]
    public string? Category { get; set; }

    [MaxLength(200)]
    public string? Tags { get; set; }

    [MaxLength(100)]
    public string? PrimaryColor { get; set; }

    [MaxLength(100)]
    public string? SecondaryColor { get; set; }

    public int SortOrder { get; set; } = 0;

    public decimal MinimumPrice { get; set; } = 0;

    [MaxLength(10)]
    public string Currency { get; set; } = "USD";

    [Column(TypeName = "jsonb")]
    public string? TechnicalRequirements { get; set; }

    [Column(TypeName = "jsonb")]
    public string? Features { get; set; }

    [Column(TypeName = "jsonb")]
    public string? Integrations { get; set; }

    [MaxLength(200)]
    public string? SupportEmail { get; set; }

    [MaxLength(500)]
    public string? DocumentationUrl { get; set; }

    [MaxLength(500)]
    public string? ApiDocumentationUrl { get; set; }

    [MaxLength(500)]
    public string? SupportUrl { get; set; }

    [MaxLength(500)]
    public string? TermsOfServiceUrl { get; set; }

    [MaxLength(500)]
    public string? PrivacyPolicyUrl { get; set; }

    public DateTime? LaunchDate { get; set; }

    public DateTime? LastUpdated { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public bool RequiresApproval { get; set; } = false;

    [MaxLength(100)]
    public string? DeveloperName { get; set; }

    [MaxLength(500)]
    public string? DeveloperWebsite { get; set; }

    public int? MaxConcurrentUsers { get; set; }

    public bool SupportsSSO { get; set; } = false;

    public bool SupportsAPI { get; set; } = false;

    public bool SupportsMobile { get; set; } = false;

    public bool SupportsOffline { get; set; } = false;

    [MaxLength(100)]
    public string? DatabaseType { get; set; }

    [MaxLength(100)]
    public string? HostingProvider { get; set; }

    [MaxLength(50)]
    public string? SecurityCertifications { get; set; }

    public bool IsGDPRCompliant { get; set; } = false;

    public bool IsSOC2Compliant { get; set; } = false;

    [Column(TypeName = "jsonb")]
    public string? Metadata { get; set; }

    public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    public ICollection<SoftwareReview> Reviews { get; set; } = new List<SoftwareReview>();
    public ICollection<SoftwareFeature> SoftwareFeatures { get; set; } = new List<SoftwareFeature>();
}
