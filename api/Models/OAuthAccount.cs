using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class OAuthAccount
{

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    public OAuthProvider OAuthProvider { get; set; }

    [StringLength(500)]
    public string ProviderUrl { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string RedirectUrl { get; set; } = string.Empty;

    [StringLength(100)]
    public string? OAuthProviderUserId { get; set; }

    [StringLength(2000)]
    public string? AccessToken { get; set; }

    [StringLength(2000)]
    public string? RefreshToken { get; set; }

    public DateTime? ExpiresAt { get; set; }


    [StringLength(1000)]
    public string? Scope { get; set; }

    [StringLength(100)]
    public string? TokenType { get; set; } = "Bearer";

    public DateTime? RefreshTokenExpiresAt { get; set; }

    [StringLength(500)]
    public string? IdToken { get; set; }


    // User profile information from OAuth provider
    [StringLength(200)]
    public string? ProviderDisplayName { get; set; }

    [StringLength(200)]
    public string? ProviderEmail { get; set; }

    [StringLength(500)]
    public string? ProviderAvatarUrl { get; set; }

    [Column(TypeName = "jsonb")]
    public string? ProviderProfileData { get; set; }




    // Account status and management

    public bool IsActive { get; set; } = true;

    public bool IsVerified { get; set; } = false;

    public DateTime? VerifiedAt { get; set; }

    public bool IsPrimary { get; set; } = false;

    [StringLength(50)]
    public string Status { get; set; } = "Active"; // Active, Suspended, Revoked, Expired


    // Security and audit
    public DateTime? LastTokenRefresh { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public int RefreshAttempts { get; set; } = 0;

    public DateTime? LastRefreshAttempt { get; set; }

    public bool IsRevokedByUser { get; set; } = false;

    public DateTime? RevokedAt { get; set; }

    public Guid? RevokedById { get; set; }

    [ForeignKey(nameof(RevokedById))]
    public User? RevokedBy { get; set; }

    [StringLength(500)]
    public string? RevocationReason { get; set; }


    // Rate limiting and throttling
    public int TokenRequestCount { get; set; } = 0;

    public DateTime? LastTokenRequest { get; set; }

    public DateTime? ThrottledUntil { get; set; }


    // Error tracking
    [StringLength(1000)]
    public string? LastError { get; set; }

    public DateTime? LastErrorAt { get; set; }

    public int ConsecutiveErrorCount { get; set; } = 0;


    // Metadata
    [StringLength(200)]
    public string? ClientId { get; set; }

    [StringLength(100)]
    public string? ProviderVersion { get; set; }

    [Column(TypeName = "jsonb")]
    public string? AdditionalMetadata { get; set; }


// Timestamps
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastSyncAt { get; set; }

    // Computed properties
    [NotMapped]
    public bool IsTokenExpired => ExpiresAt.HasValue && ExpiresAt.Value <= DateTime.UtcNow;

    [NotMapped]
    public bool IsRefreshTokenExpired => RefreshTokenExpiresAt.HasValue && RefreshTokenExpiresAt.Value <= DateTime.UtcNow;

    [NotMapped]
    public bool NeedsRefresh => IsTokenExpired && !IsRefreshTokenExpired && !string.IsNullOrEmpty(RefreshToken);

    [NotMapped]
    public bool IsThrottled => ThrottledUntil.HasValue && ThrottledUntil.Value > DateTime.UtcNow;

    [NotMapped]
    public TimeSpan? TimeUntilExpiry => ExpiresAt?.Subtract(DateTime.UtcNow);

    [NotMapped]
    public string ProviderDisplayText => OAuthProvider.ToString();

    // Business logic methods
    public void UpdateTokens(string accessToken, string? refreshToken = null, int expiresInSeconds = 3600, string? scope = null)
    {
        AccessToken = accessToken;

        if (!string.IsNullOrEmpty(refreshToken))
            RefreshToken = refreshToken;

        ExpiresAt = DateTime.UtcNow.AddSeconds(expiresInSeconds);

        if (!string.IsNullOrEmpty(scope))
            Scope = scope;

        LastTokenRefresh = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        // Reset error tracking on successful token update
        ConsecutiveErrorCount = 0;
        LastError = null;
        LastErrorAt = null;
    }

    public void MarkAsUsed()
    {
        LastUsedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordError(string error)
    {
        LastError = error;
        LastErrorAt = DateTime.UtcNow;
        ConsecutiveErrorCount++;
        UpdatedAt = DateTime.UtcNow;

        // Auto-suspend after too many consecutive errors
        if (ConsecutiveErrorCount >= 5)
        {
            Status = "Suspended";
        }
    }

    public void RecordRefreshAttempt()
    {
        RefreshAttempts++;
        LastRefreshAttempt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Revoke(Guid? revokedById = null, string? reason = null)
    {
        IsRevokedByUser = true;
        RevokedAt = DateTime.UtcNow;
        RevokedById = revokedById;
        RevocationReason = reason;
        Status = "Revoked";
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;

        // Clear sensitive data
        AccessToken = null;
        RefreshToken = null;
        IdToken = null;
    }

    public void SetAsPrimary()
    {
        IsPrimary = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProviderProfile(string? displayName, string? email, string? avatarUrl, string? profileDataJson = null)
    {
        ProviderDisplayName = displayName;
        ProviderEmail = email;
        ProviderAvatarUrl = avatarUrl;
        ProviderProfileData = profileDataJson;
        LastSyncAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool CanRefreshToken()
    {
        return !string.IsNullOrEmpty(RefreshToken) &&
               !IsRefreshTokenExpired &&
               IsActive &&
               Status == "Active" &&
               !IsRevokedByUser;
    }



    // Index configuration
    public static void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        // Unique constraint: one account per provider per user
        modelBuilder.Entity<OAuthAccount>()
            .HasIndex(o => new { o.UserId, o.OAuthProvider })
            .IsUnique()
            .HasDatabaseName("IX_OAuthAccount_User_Provider");

        modelBuilder.Entity<OAuthAccount>()
            .HasIndex(o => o.OAuthProviderUserId)
            .HasDatabaseName("IX_OAuthAccount_ProviderUserId");

        modelBuilder.Entity<OAuthAccount>()
            .HasIndex(o => o.ProviderEmail)
            .HasDatabaseName("IX_OAuthAccount_ProviderEmail");

        modelBuilder.Entity<OAuthAccount>()
            .HasIndex(o => o.ExpiresAt)
            .HasDatabaseName("IX_OAuthAccount_ExpiresAt");

        modelBuilder.Entity<OAuthAccount>()
            .HasIndex(o => o.IsActive)
            .HasDatabaseName("IX_OAuthAccount_IsActive");

        modelBuilder.Entity<OAuthAccount>()
            .HasIndex(o => o.Status)
            .HasDatabaseName("IX_OAuthAccount_Status");

        modelBuilder.Entity<OAuthAccount>()
            .HasIndex(o => o.IsPrimary)
            .HasDatabaseName("IX_OAuthAccount_IsPrimary");
    }



    // Model configuration
    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OAuthAccount>()
            .HasOne(o => o.User)
            .WithMany(u => u.OAuthAccounts)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OAuthAccount>()
            .HasOne(o => o.RevokedBy)
            .WithMany()
            .HasForeignKey(o => o.RevokedById)
            .OnDelete(DeleteBehavior.SetNull);
    }


}
