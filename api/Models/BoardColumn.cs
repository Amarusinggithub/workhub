using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class BoardColumn
{
    [Key]
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = "Board Column";

    public int Order { get; set; }


public Guid BoardId { get; set; }
[ForeignKey(nameof(BoardId) )]

    public Board Board { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;


    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoardColumn>()
            .HasOne(iug => iug.Board)
            .WithMany(ug => ug.Columns)
            .HasForeignKey(iug => iug.BoardId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

