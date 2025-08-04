using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class WorkspaceRole
{
    [Key]
    public int Id { get; set; }


    [Required]
    public Guid  WorkspaceId { get; set; }
    [ForeignKey(nameof(WorkspaceId))]

    public Workspace Workspace { get; set; }

    [Required]
    public Guid CreatedById { get; set; }
    [ForeignKey(nameof(CreatedById))]

    public User Createdby { get; set; }

    [Required]
    public Guid RoleId { get; set; }
    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
    public ICollection<WorkspaceMemberShip> WorkSpaceMembers { get; set; } = new List<WorkspaceMemberShip>();

     public static void ConfigureRelations(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkspaceRole>()
                .HasOne(uwr => uwr.Role)
                .WithMany(u => u.WorkspaceRoles)
                .HasForeignKey(uwr => uwr.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkspaceRole>()
                .HasOne(uwr => uwr.Workspace)
                .WithMany(w => w.WorkspaceRoles)
                .HasForeignKey(uwr => uwr.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkspaceRole>()
                .HasOne(uwr => uwr.Createdby)
                .WithMany()
                .HasForeignKey(uwr => uwr.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);
        }

}
