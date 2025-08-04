using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(options)
{
    public DbSet<Resource> Resources { get; set; }
    public DbSet<UserResource> UserResources { get; set; }

    public DbSet<ProjectAttachment> ProjectAttachments { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<GroupInvitation> UserGroupInvitations { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    public DbSet<UserGroupType> UserGroupTypes { get; set; }
    public DbSet<GroupMembership> GroupMemberships { get; set; }
    public DbSet<Workspace> WorkSpaces { get; set; }
    public DbSet<WorkspaceInvitation> WorkspaceInvitations { get; set; }

    public DbSet<WorkspaceMembership> WorkSpaceMemberships { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMembership> ProjectMemberships { get; set; }
    public DbSet<ProjectCategory> ProjectCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<WorkspaceRole> WorkspaceRoles { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<TaskAttachment> TaskAttachments { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }


    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationMembership> NotificationMemberships { get; set; }
    public DbSet<OAuthAccount> OAuthAccounts { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<SoftwareFeature> SoftwareFeatures { get; set; }
    public DbSet<SoftwareReview> SoftwareReviews { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardColumn> BoardColumns { get; set; }

    public DbSet<Plan> Plans { get; set; }
    public DbSet<Include> Includes { get; set; }
    public DbSet<Prerequisite> Prerequisites { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<OptionIncluded> OptionIncludes { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentHistory> CommentHistories { get; set; }
    public DbSet<CommentMention> CommentMentions { get; set; }
    public DbSet<CommentAttachment> CommentAttachments { get; set; }
    public DbSet<CommentReaction> CommentReactions { get; set; }


    public DbSet<Label> Labels { get; set; }
    public DbSet<TaskLabel> TaskLabels { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<PlanHistory> PlanHistories { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<EntityPropertyChange> EntityPropertyChanges { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
       // Comment
        Comment.ConfigureRelations(modelBuilder);
        Comment.ConfigureIndexes(modelBuilder);

        // OAuthAccount
        OAuthAccount.ConfigureRelations(modelBuilder);
        OAuthAccount.ConfigureIndexes(modelBuilder);


        // TaskItem
        TaskItem.ConfigureRelations(modelBuilder);
        //TaskItem.ConfigureIndexes(modelBuilder);

        // TaskAssignment
        TaskAssignment.ConfigureRelations(modelBuilder);
        //TaskAssignment.ConfigureIndexes(modelBuilder);

        // TaskAttachment
        TaskAttachment.ConfigureRelations(modelBuilder);
        //TaskAttachment.ConfigureIndexes(modelBuilder);

        // TaskLabel
        TaskLabel.ConfigureRelations(modelBuilder);
        //TaskLabel.ConfigureIndexes(modelBuilder);

        // Project
        Project.ConfigureRelations(modelBuilder);
        //Project.ConfigureIndexes(modelBuilder);

        // ProjectAttachment
        ProjectAttachment.ConfigureRelations(modelBuilder);
        //ProjectAttachment.ConfigureIndexes(modelBuilder);

        // ProjectMembership
        ProjectMembership.ConfigureRelations(modelBuilder);
        //ProjectMembership.ConfigureIndexes(modelBuilder);

        // Workspace
        Workspace.ConfigureRelations(modelBuilder);
        //Workspace.ConfigureIndexes(modelBuilder);

        // WorkspaceMembership
        WorkspaceMembership.ConfigureRelations(modelBuilder);
        //WorkspaceMembership.ConfigureIndexes(modelBuilder);

        // WorkspaceRole
        WorkspaceRole.ConfigureRelations(modelBuilder);
        //WorkspaceRole.ConfigureIndexes(modelBuilder);

        // WorkspaceInvitation
        WorkspaceInvitation.ConfigureRelations(modelBuilder);
        //WorkspaceInvitation.ConfigureIndexes(modelBuilder);

        // GroupMembership
        GroupMembership.ConfigureRelations(modelBuilder);
        //GroupMembership.ConfigureIndexes(modelBuilder);

        // GroupInvitation
        GroupInvitation.ConfigureRelations(modelBuilder);
        //GroupInvitation.ConfigureIndexes(modelBuilder);

        // Resource
        Resource.ConfigureRelations(modelBuilder);
        //Resource.ConfigureIndexes(modelBuilder);

        // NotificationMembership
        NotificationMembership.ConfigureRelations(modelBuilder);
       // NotificationMembership.ConfigureIndexes(modelBuilder);

        // Include
        Include.ConfigureRelations(modelBuilder);
        //Include.ConfigureIndexes(modelBuilder);

        // Prerequisite
        Prerequisite.ConfigureRelations(modelBuilder);
        //Prerequisite.ConfigureIndexes(modelBuilder);

        // OptionIncluded
        OptionIncluded.ConfigureRelations(modelBuilder);
        //OptionIncluded.ConfigureIndexes(modelBuilder);

        // Offer
        Offer.ConfigureRelations(modelBuilder);
        //Offer.ConfigureIndexes(modelBuilder);

        // PlanHistory
        PlanHistory.ConfigureRelations(modelBuilder);
        //PlanHistory.ConfigureIndexes(modelBuilder);

        // Invoice
        Invoice.ConfigureRelations(modelBuilder);
        //Invoice.ConfigureIndexes(modelBuilder);

        // SoftwareFeature
        SoftwareFeature.ConfigureRelations(modelBuilder);
        //SoftwareFeature.ConfigureIndexes(modelBuilder);

        // SoftwareReview
        SoftwareReview.ConfigureRelations(modelBuilder);
        //SoftwareReview.ConfigureIndexes(modelBuilder);

        // UserRole
        UserRole.ConfigureRelations(modelBuilder);
        //UserRole.ConfigureIndexes(modelBuilder);

        // UserPermission
        UserPermission.ConfigureRelations(modelBuilder);
        //UserPermission.ConfigureIndexes(modelBuilder);

        // UserSession
        UserSession.ConfigureRelations(modelBuilder);
        //UserSession.ConfigureIndexes(modelBuilder);

        // CommentAttachment
        CommentAttachment.ConfigureRelations(modelBuilder);
        //CommentAttachment.ConfigureIndexes(modelBuilder);

        // CommentReaction
        CommentReaction.ConfigureRelations(modelBuilder);
        //CommentReaction.ConfigureIndexes(modelBuilder);

        // CommentMention
        CommentMention.ConfigureRelations(modelBuilder);
        //CommentMention.ConfigureIndexes(modelBuilder);

        // ProjectCategory
        ProjectCategory.ConfigureRelations(modelBuilder);
        //ProjectCategory.ConfigureIndexes(modelBuilder);

        // Subscription
        Subscription.ConfigureRelations(modelBuilder);
        //Subscription.ConfigureIndexes(modelBuilder);

        // Board
        Board.ConfigureRelations(modelBuilder);
        //Board.ConfigureIndexes(modelBuilder);

        modelBuilder.Entity<User>().ToTable("AspNetUsers");
        }

    }
