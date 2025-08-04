using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }
    public Guid TaskItemId { get; set; }
    [ForeignKey(nameof(TaskItemId))]

    public TaskItem TaskItem { get; set; }

    public Guid ? AssignedToUserId { get; set; }
    [ForeignKey(nameof(AssignedToUserId))]

    public User? AssignedToUser { get; set; }

    public Guid  AssignedByUserId { get; set; }
    [ForeignKey(nameof(AssignedByUserId))]

    public User AssignedByUser { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TaskAssignment>()
            .HasOne(pre => pre.TaskItem)
            .WithMany(o => o.Assignments)
            .HasForeignKey(pre => pre.TaskItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskAssignment>()
            .HasOne(pre => pre.AssignedByUser)
            .WithMany(o => o.CreatedAssignedTasks)
            .HasForeignKey(pre => pre.AssignedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TaskAssignment>()
            .HasOne(pre => pre.AssignedToUser)
            .WithMany(o => o.AssignedToTasks)
            .HasForeignKey(pre => pre.AssignedToUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

