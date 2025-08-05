using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api.Enums;
using Microsoft.AspNetCore.Identity;
namespace api.Models;

public class User: IdentityUser<Guid >
{
    [Required]
    [EmailAddress]
    [StringLength(256)]
    public string Email { get; set; }

    [Required]
    [StringLength(256)]
    public override string UserName { get; set; } = string.Empty;


    [Required]
    [StringLength(100, MinimumLength = 1)]
   public string FirstName { get; set; }

    [StringLength(100)]
    public string? MiddleName { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
   public string LastName { get; set; }

    [StringLength(100)]
    public string? DisplayName { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [StringLength(20)]
    public string? PreferredPronoun { get; set; }
    [Url]
    [StringLength(500)]
    public string? AvatarUrl { get; set; }

    [Url]
    [StringLength(500)]
    public string? HeaderImageUrl { get; set; }

    [StringLength(150)]
    public string? JobTitle { get; set; }

    [StringLength(200)]
    public string? Department { get; set; }


    [StringLength(200)]
    public string? Organization { get; set; }

    [StringLength(100)]
    public string? EmployeeId { get; set; }


    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [StringLength(50)]
    public string? EmploymentType { get; set; }

    [StringLength(200)]
    public string? Location { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(20)]
    public string? TimeZone { get; set; }

    [Phone]
    [StringLength(20)]
    public string? MobilePhone { get; set; }

    [Phone]
    [StringLength(20)]
    public string? WorkPhone { get; set; }


    [StringLength(1000)]
    public string? Bio { get; set; }

    [StringLength(500)]
    public string? Skills { get; set; }

    [Url]
    [StringLength(200)]
    public string? LinkedInUrl { get; set; }

    [Url]
    [StringLength(200)]
    public string? GitHubUrl { get; set; }

    [Url]
    [StringLength(200)]
    public string? PersonalWebsiteUrl { get; set; }

    [StringLength(2000)]
    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiresAtUtc { get; set; }

    public DateTime? PasswordLastChanged { get; set; }

    public bool RequirePasswordChange { get; set; }

    public int FailedLoginAttempts { get; set; }

    public DateTime? LastFailedLoginAttempt { get; set; }

    public DateTime? AccountLockedUntil { get; set; }

    public bool IsTwoFactorEnabled { get; set; }

    [StringLength(100)]
    public string? TwoFactorBackupCodes { get; set; }

    public DateTime? LastPasswordResetRequest { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedById { get; set; }

    [ForeignKey(nameof(DeletedById))]
    public User? DeletedBy { get; set; }

    [StringLength(500)]
    public string? DeactivationReason { get; set; }

    public bool IsSystemUser { get; set; }

    public bool IsBot { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = "Active";

    [StringLength(10)]
    public string PreferredLanguage { get; set; } = "en";

    public Theme Theme { get; set; } = Theme.System;

    public bool EmailNotificationsEnabled { get; set; } = true;

    public bool PushNotificationsEnabled { get; set; } = true;

    public bool MarketingEmailsEnabled { get; set; }

    [Column(TypeName = "jsonb")]
    public string? NotificationPreferences { get; set; }

    [Column(TypeName = "jsonb")]
    public string? UserPreferences { get; set; }


    public DateTime? LastLoggedIn { get; set; }

    public DateTime? LastActivityAt { get; set; }

    [StringLength(45)]
    public string? LastLoginIpAddress { get; set; }

    [StringLength(500)]
    public string? LastLoginUserAgent { get; set; }

    public int LoginCount { get; set; }

    public DateTime? LastSeenAt { get; set; }


    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CreatedById { get; set; }

    [ForeignKey(nameof(CreatedById))]
    public User? CreatedBy { get; set; }


    public DateTime? TermsAcceptedAt { get; set; }

    [StringLength(20)]
    public string? TermsVersion { get; set; }

    public DateTime? PrivacyPolicyAcceptedAt { get; set; }

    [StringLength(20)]
    public string? PrivacyPolicyVersion { get; set; }

    [Required]
    public ICollection<TaskItem> Issues { get; set; } = new List<TaskItem>();


    public ICollection<WorkspaceMemberShip> CreatedWorkspaceMemberShips { get; set; } = new List<WorkspaceMemberShip>();
    public ICollection<WorkspaceMemberShip> WorkspaceMemberShips { get; set; } = new List<WorkspaceMemberShip>();


    public ICollection<UserResource> UserResources { get; set; } = new List<UserResource>();

    public ICollection<ProjectMemberShip> CreatedProjectMemberShips { get; set; } = new List<ProjectMemberShip>();

    public ICollection<TaskAssignment> AssignedToTasks { get; set; } = new List<TaskAssignment>();
    public ICollection<TaskAssignment> CreatedAssignedTasks { get; set; } = new List<TaskAssignment>();

    public ICollection<TaskItem> CreatedTasks { get; set; } = new List<TaskItem>();

    public ICollection<TaskAttachment> CreatedTaskAttachments { get; set; } = new List<TaskAttachment>();
    public ICollection<ProjectAttachment> CreatedProjectAttachments { get; set; } = new List<ProjectAttachment>();



    public ICollection<GroupMemberShip> CreatedGroupMemberShips { get; set; } = new List<GroupMemberShip>();

    public ICollection<GroupMemberShip> GroupMemberShips { get; set; } = new List<GroupMemberShip>();
    public ICollection<CommentAttachment> CreatedCommentAttachments { get; set; } = new List<CommentAttachment>();


    public ICollection<Board> CreatedBoards { get; set; } = new List<Board>();

    public ICollection<Comment> CreatedComments { get; set; } = new List<Comment>();
    public ICollection<Comment> PinnedComments { get; set; } = new List<Comment>();

    public ICollection<Comment> ApprovedComments { get; set; } = new List<Comment>();


    public ICollection<ProjectMemberShip> ProjectMemberShips { get; set; } = new List<ProjectMemberShip>();

    public ICollection<WorkspaceRole> WorkspaceRoles { get; set; } = new List<WorkspaceRole>();

    public ICollection<NotificationMemberShip> NotificationMemberships { get; set; } = new List<NotificationMemberShip>();


    public ICollection<OAuthAccount> OAuthAccounts { get; set; } = new List<OAuthAccount>();
    public ICollection<Workspace> CreatedWorkspaces { get; set; } = new List<Workspace>();
    public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();
    public ICollection<Resource> UploadedResources { get; set; } = new List<Resource>();
    public ICollection<Resource> DeletedResources { get; set; } = new List<Resource>();

    public ICollection<GroupInvitation> GroupInvitationsSent { get; set; } = new List<GroupInvitation>();

    public ICollection<GroupInvitation> GroupInvitationsReceived { get; set; } = new List<GroupInvitation>();

    public ICollection<WorkspaceInvitation> WorkspaceInvitationsReceived { get; set; } = new List<WorkspaceInvitation>();

    public ICollection<WorkspaceInvitation> WorkspaceInvitationsSent { get; set; } = new List<WorkspaceInvitation>();

    public ICollection<AuditLog> PerformedAudits { get; set; } = new List<AuditLog>();


    public ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();

    public ICollection<UserRole> CustomRoles { get; set; } = new List<UserRole>();

    public ICollection<UserPermission> CustomPermissions { get; set; } = new List<UserPermission>();

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();

    [NotMapped]
    public string DisplayNameOrFullName => !string.IsNullOrEmpty(DisplayName) ? DisplayName : FullName;

    [NotMapped]
    public string Initials => $"{FirstName?.FirstOrDefault()}{LastName?.FirstOrDefault()}".ToUpper();

    [NotMapped]
    public bool IsOnline => LastSeenAt.HasValue && LastSeenAt.Value > DateTime.UtcNow.AddMinutes(-5);

    [NotMapped]
    public bool IsAccountLocked => AccountLockedUntil.HasValue && AccountLockedUntil.Value > DateTime.UtcNow;

    [NotMapped]
    public bool IsRefreshTokenValid => !string.IsNullOrEmpty(RefreshToken) &&
                                       RefreshTokenExpiresAtUtc.HasValue &&
                                       RefreshTokenExpiresAtUtc.Value > DateTime.UtcNow;

    [NotMapped]
    public int Age => DateOfBirth.HasValue ?
        DateTime.Today.Year - DateOfBirth.Value.Year -
        (DateTime.Today.DayOfYear < DateOfBirth.Value.DayOfYear ? 1 : 0) : 0;

    [NotMapped]
    public TimeSpan? TimeSinceLastActivity => LastActivityAt.HasValue ?
        DateTime.UtcNow - LastActivityAt.Value : null;

    public void UpdateActivity()
    {
        LastActivityAt = DateTime.UtcNow;
        LastSeenAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordLogin(string? ipAddress = null, string? userAgent = null)
    {
        LastLoggedIn = DateTime.UtcNow;
        LastActivityAt = DateTime.UtcNow;
        LastSeenAt = DateTime.UtcNow;
        LastLoginIpAddress = ipAddress;
        LastLoginUserAgent = userAgent;
        LoginCount++;
        FailedLoginAttempts = 0;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RecordFailedLogin()
    {
        FailedLoginAttempts++;
        LastFailedLoginAttempt = DateTime.UtcNow;

        // Lock account after 5 failed attempts for 30 minutes
        if (FailedLoginAttempts >= 5)
        {
            AccountLockedUntil = DateTime.UtcNow.AddMinutes(30);
        }

        UpdatedAt = DateTime.UtcNow;
    }

    public void SetRefreshToken(string token, DateTime validity)
    {
        RefreshToken = token;
        RefreshTokenExpiresAtUtc = validity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ClearRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiresAtUtc = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete(Guid? deletedById = null, string? reason = null)
    {
        IsDeleted = true;
        IsActive = false;
        DeletedAt = DateTime.UtcNow;
        DeletedById = deletedById;
        DeactivationReason = reason;
        Status = "Deleted";
        UpdatedAt = DateTime.UtcNow;

        ClearRefreshToken();
    }

    public void Reactivate()
    {
        IsDeleted = false;
        IsActive = true;
        DeletedAt = null;
        DeletedById = null;
        DeactivationReason = null;
        Status = "Active";
        UpdatedAt = DateTime.UtcNow;
    }

    public void AcceptTerms(string version)
    {
        TermsAcceptedAt = DateTime.UtcNow;
        TermsVersion = version;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AcceptPrivacyPolicy(string version)
    {
        PrivacyPolicyAcceptedAt = DateTime.UtcNow;
        PrivacyPolicyVersion = version;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UnlockAccount()
    {
        AccountLockedUntil = null;
        FailedLoginAttempts = 0;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool HasAcceptedCurrentTerms(string currentVersion)
    {
        return TermsAcceptedAt.HasValue && TermsVersion == currentVersion;
    }

    public bool HasAcceptedCurrentPrivacyPolicy(string currentVersion)
    {
        return PrivacyPolicyAcceptedAt.HasValue && PrivacyPolicyVersion == currentVersion;
    }


    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}
