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
    public DbSet<WorkspaceRole> WorkspaceRoles { get; set; }

    public DbSet<ProjectResource> ProjectResources { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserGroupInvitation> UserGroupInvitations { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    public DbSet<UserGroupType> UserGroupTypes { get; set; }
    public DbSet<UserGroupMember> UserGroupMembers { get; set; }
    public DbSet<Workspace> WorkSpaces { get; set; }
    public DbSet<WorkspaceInvitation> WorkspaceInvitations { get; set; }

    public DbSet<WorkSpaceMember> WorkSpaceMembers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<ProjectCategory> ProjectCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<WorkspaceRole> UserWorkspaeRoles { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<TaskAttachment> TaskAttachments { get; set; }
    public DbSet<TaskAssignment> TaskAssignments { get; set; }


    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationMember> NotificationMembers { get; set; }
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


    protected override void OnModelCreating(ModelBuilder builder)
    {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("AspNetUsers");

            builder.Entity<Comment>()
                .HasOne(co => co.Commenter)
                .WithMany(o => o.Comments)
                .HasForeignKey(inc => inc.CommenterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(issue => issue.TaskItem)
                .WithMany(p => p.Comments)
                .HasForeignKey(inc => inc.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskLabel>()
                .HasOne(isl => isl.Label)
                .WithMany(l => l.Issues)
                .HasForeignKey(il => il.LabelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskLabel>()
                .HasOne(issue => issue.TaskItem)
                .WithMany(p => p.Labels)
                .HasForeignKey(inc => inc.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectResource>()
                .HasOne(ugr => ugr.Resource)
                .WithMany(r => r.UserResources)
                .HasForeignKey(ugr => ugr.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectResource>()
                .HasOne(pr => pr.Workspace)
                .WithMany(w => w.ProjectResources)
                .HasForeignKey(ugr => ugr.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskAttachment>()
                .HasOne(ts => ts.Resource)
                .WithMany(r => r.TaskResources)
                .HasForeignKey(ugr => ugr.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskAttachment>()
                .HasOne(ugr => ugr.TaskItem)
                .WithMany(u => u.Attactments)
                .HasForeignKey(ugr => ugr.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroupMember>()
                .HasOne(iug => iug.UserGroup)
                .WithMany(ug => ug.Users)
                .HasForeignKey(iug => iug.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroupInvitation>()
                .HasOne(iug => iug.UserGroup)
                .WithMany(ug => ug.Invitations)
                .HasForeignKey(iug => iug.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroupInvitation>()
                .HasOne(iug => iug.InvitedBy)
                .WithMany(ug => ug.UserGroupInvitations)
                .HasForeignKey(iug => iug.InvitedByUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<WorkspaceInvitation>()
                .HasOne(iug => iug.InvitedBy)
                .WithMany(ug => ug.WorkspaceInvitations)
                .HasForeignKey(iug => iug.InvitedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkspaceInvitation>()
                .HasOne(iug => iug.Workspace)
                .WithMany(ug => ug.Invitations)
                .HasForeignKey(iug => iug.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroupMember>()
                .HasOne(iug => iug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(iug => iug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkSpaceMember>()
                .HasOne(iws => iws.Workspace)
                .WithMany(ws => ws.WorkSpaceMembers)
                .HasForeignKey(iws => iws.WorkSpaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkSpaceMember>()
                .HasOne(iws => iws.User)
                .WithMany(u => u.WorkspaceMemberships)
                .HasForeignKey(iws => iws.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectMember>()
                .HasOne(inp => inp.Project)
                .WithMany(p => p.ProjectMembers)
                .HasForeignKey(inp => inp.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectMember>()
                .HasOne(inp => inp.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(inp => inp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<ProjectCategory>()
                    .HasOne(pc => pc.Project)
                    .WithMany(p => p.ProjectCategories)
                    .HasForeignKey(pc => pc.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<ProjectCategory>()
                    .HasOne(pc => pc.Category)
                    .WithMany(c => c.ProjectCategories)
                    .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

                builder.Entity<WorkspaceRole>()
                    .HasOne(uwr => uwr.User)
                    .WithMany(u => u.UserWorkspaceRoles)
                    .HasForeignKey(uwr => uwr.UserId)
               .OnDelete(DeleteBehavior.Cascade);

               builder.Entity<WorkspaceRole>()
                   .HasOne(uwr => uwr.Workspace)
                   .WithMany(w => w.WorkspaceRoles)
                   .HasForeignKey(uwr => uwr.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);

               builder.Entity<NotificationMember>()
                   .HasOne(u => u.User)
                   .WithMany(u => u.UserNotifications)
                   .HasForeignKey(u=> u.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

               builder.Entity<NotificationMember>()
                   .HasOne(n => n.Notification)
                   .WithMany(u => u.UserNotifications)
                   .HasForeignKey(n=> n.NotificationId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Include>()
                .HasOne(inc => inc.Offer)
                .WithMany(o => o.Includes)
                .HasForeignKey(inc => inc.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Include>()
                .HasOne(inc => inc.Plan)
                .WithMany(p => p.Includes)
                .HasForeignKey(inc => inc.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskAssignment>()
                .HasOne(pre => pre.TaskItem)
                .WithMany(o => o.Assignments)
                .HasForeignKey(pre => pre.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskAssignment>()
                .HasOne(pre => pre.AssignedByUser)
                .WithMany(o => o.CreatedTasks)
                .HasForeignKey(pre => pre.AssignedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Prerequisite>()
                .HasOne(pre => pre.Offer)
                .WithMany(o => o.Prerequisites)
                .HasForeignKey(pre => pre.OfferId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Prerequisite>()
                .HasOne(pre => pre.Plan)
                .WithMany(p => p.Prerequisites)
                .HasForeignKey(pre => pre.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OptionIncluded>()
                .HasOne(oi => oi.Plan)
                .WithMany(p => p.OptionIncludes)
                .HasForeignKey(oi => oi.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OptionIncluded>()
                .HasOne(oi => oi.Option)
                .WithMany(o => o.OptionIncludes)
                .HasForeignKey(oi => oi.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Offer>()
                .HasMany(o => o.Subscriptions)
                .WithOne(s => s.Offer)
                .HasForeignKey(s => s.OfferId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<PlanHistory>()
                .HasOne(ph => ph.Subscription)
                .WithMany(s => s.PlanHistories)
                .HasForeignKey(ph => ph.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PlanHistory>()
                .HasOne(ph => ph.Plan)
                .WithMany(p => p.PlanHistories)
                .HasForeignKey(ph => ph.PlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Invoice>()
                .HasOne(inv => inv.Subscription)
                .WithMany(s => s.Invoices)
                .HasForeignKey(inv => inv.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Invoice>()
                .HasOne(inv => inv.PlanHistory)
                .WithMany(ph => ph.Invoices)
                .HasForeignKey(inv => inv.PlanHistoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SoftwareFeature>()
                .HasOne(iug => iug.Software)
                .WithMany(ug => ug.SoftwareFeatures)
                .HasForeignKey(iug => iug.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SoftwareReview>()
                .HasOne(iug => iug.Software)
                .WithMany(ug => ug.Reviews)
                .HasForeignKey(iug => iug.SoftwareId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserRole>()
                .HasOne(iug => iug.User)
                .WithMany(ug => ug.CustomRoles)
                .HasForeignKey(iug => iug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserPermission>()
                .HasOne(iug => iug.User)
                .WithMany(ug => ug.CustomPermissions)
                .HasForeignKey(iug => iug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserSession>()
                .HasOne(iug => iug.User)
                .WithMany(ug => ug.Sessions)
                .HasForeignKey(iug => iug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentAttachment>()
                .HasOne(iug => iug.Comment)
                .WithMany(ug => ug.Attachments)
                .HasForeignKey(iug => iug.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentAttachment>()
                .HasOne(iug => iug.Attactment)
                .WithMany(ug => ug.CommentAttachments)
                .HasForeignKey(iug => iug.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentReaction>()
                .HasOne(iug => iug.Comment)
                .WithMany(ug => ug.Reactions)
                .HasForeignKey(iug => iug.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentReaction>()
                .HasOne(iug => iug.User)
                .WithMany()
                .HasForeignKey(iug => iug.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<CommentMention>()
                .HasOne(iug => iug.Comment)
                .WithMany(ug => ug.Mentions)
                .HasForeignKey(iug => iug.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentMention>()
                .HasOne(iug => iug.MentionedUser)
                .WithMany()
                .HasForeignKey(iug => iug.MentionedUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
