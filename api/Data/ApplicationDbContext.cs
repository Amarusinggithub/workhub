using api.Models;
using Task=api.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models_Task = api.Models.Task;

namespace api.Database;

public class ApplicationDbContext: IdentityDbContext<User,Role,int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
    ) : base(options)
    {

    }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<UserGroupResource> UserGroupResources { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    public DbSet<UserGroupType> UserGroupTypes { get; set; }
    public DbSet<UserGroupMember> InUserGroups { get; set; }
    public DbSet<WorkSpace> WorkSpaces { get; set; }
    public DbSet<WorkSpaceMember> WorkSpaceMembers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectMember> ProjectMembers { get; set; }
    public DbSet<ProjectCategory> ProjectCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserProjectRole> UserProjectRoles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Models_Task> Tasks { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<NotificationMember> NotificationMembers { get; set; }
    public DbSet<OAuthAccount> OAuthAccounts { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Include> Includes { get; set; }
    public DbSet<Prerequisite> Prerequisites { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<OptionIncluded> OptionIncludes { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<PlanHistory> PlanHistories { get; set; }
    public DbSet<Invoice> Invoices { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("AspNetUsers");


            builder.Entity<UserGroupResource>()
                .HasOne(ugr => ugr.Resource)
                .WithMany(r => r.UserResource)
                .HasForeignKey(ugr => ugr.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroupResource>()
                .HasOne(ugr => ugr.UserGroup)
                .WithMany(u => u.Resources)
                .HasForeignKey(ugr => ugr.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroupMember>()
                .HasOne(iug => iug.UserGroup)
                .WithMany(ug => ug.Users)
                .HasForeignKey(iug => iug.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserGroupMember>()
                .HasOne(iug => iug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(iug => iug.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkSpaceMember>()
                .HasOne(iws => iws.WorkSpace)
                .WithMany(ws => ws.UserWorkSpaces)
                .HasForeignKey(iws => iws.WorkSpaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<WorkSpaceMember>()
                .HasOne(iws => iws.User)
                .WithMany(u => u.UserWorkSpaces)
                .HasForeignKey(iws => iws.UserId);
                //.OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectMember>()
                .HasOne(inp => inp.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(inp => inp.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ProjectMember>()
                .HasOne(inp => inp.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(inp => inp.UserId);
                //.OnDelete(DeleteBehavior.Cascade);

                builder.Entity<ProjectCategory>()
                    .HasOne(pc => pc.Project)
                    .WithMany(p => p.ProjectCategories)
                    .HasForeignKey(pc => pc.ProjectId);
                //.OnDelete(DeleteBehavior.Cascade);

                builder.Entity<ProjectCategory>()
                    .HasOne(pc => pc.Category)
                    .WithMany(c => c.ProjectCategories)
                    .HasForeignKey(pc => pc.CategoryId);
                //.OnDelete(DeleteBehavior.Cascade);

                builder.Entity<UserProjectRole>()
                    .HasOne(upr => upr.User)
                    .WithMany(u => u.UserProjectRoles)
                    .HasForeignKey(upr => upr.UserId);
               // .OnDelete(DeleteBehavior.Cascade);

               builder.Entity<UserProjectRole>()
                   .HasOne(upr => upr.Project)
                   .WithMany(p => p.UserProjectRoles)
                   .HasForeignKey(upr => upr.ProjectId);
               // .OnDelete(DeleteBehavior.Cascade);

               builder.Entity<NotificationMember>()
                   .HasOne(u => u.User)
                   .WithMany(u => u.UserNotifications)
                   .HasForeignKey(u=> u.Userid)
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
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
