using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class Board
{
    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();
    [StringLength(100)]

    public string Name { get; set; } = "Board";
    [StringLength(200)]
    public string Description { get; set; }



    public Guid? CreatedByUserId { get; set; }
    [ForeignKey(nameof(CreatedByUserId))]
    public User? CreatedBy { get; set; }

    public Guid ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public Project Project { get; set; } = null!;

    public ICollection<BoardColumn> Columns { get; set; } = new List<BoardColumn>();

    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;

    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Board>()
            .HasOne(pre => pre.CreatedBy)
            .WithMany(o => o.CreatedBoards)
            .HasForeignKey(pre => pre.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

