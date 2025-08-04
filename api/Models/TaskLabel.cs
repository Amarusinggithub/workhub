using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class TaskLabel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public Guid  TaskId { get; set; }
    [ForeignKey(nameof(TaskId))]

    public TaskItem TaskItem { get; set; }


    [Required]
    public int LabelId { get; set; }
    [ForeignKey(nameof(LabelId))]

    public Label Label { get; set; }



    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;



    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskLabel>()
            .HasOne(isl => isl.Label)
            .WithMany(l => l.Issues)
            .HasForeignKey(il => il.LabelId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TaskLabel>()
            .HasOne(issue => issue.TaskItem)
            .WithMany(p => p.Labels)
            .HasForeignKey(inc => inc.TaskId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
