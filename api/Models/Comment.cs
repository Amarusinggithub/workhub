using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid  CommenterId { get; set; }

    [ForeignKey(nameof(CommenterId))]
    public User Commenter { get; set; }

    [Required]
    [StringLength(10000, MinimumLength = 1, ErrorMessage = "Comment must be between 1 and 10000 characters")]
    public string Message { get; set; } = string.Empty;


    [StringLength(5000)]
    public string? FormattedMessage { get; set; }

    public bool IsEdited { get; set; } = false;
    public DateTime? EditedAt { get; set; }

    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedById { get; set; }

    [ForeignKey(nameof(DeletedById))]
    public User? DeletedBy { get; set; }

    public ICollection<Comment> Replies { get; set; } = new List<Comment>();

    public Guid? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public Comment? Parent { get; set; }

    [Range(0, 15, ErrorMessage = "Thread depth cannot exceed 15 levels")]
    public int ThreadDepth { get; set; } = 0;
    public ICollection<CommentAttachment> Attachments { get; set; } = new List<CommentAttachment>();
    public ICollection<CommentMention> Mentions { get; set; } = new List<CommentMention>();


    public bool IsPinned { get; set; } = false;
    public DateTime? PinnedAt { get; set; }
    public Guid? PinnedById { get; set; }

    [ForeignKey(nameof(PinnedById))]
    public User? PinnedBy { get; set; }

    [Required]
    public Guid TaskId { get; set; }

    [ForeignKey(nameof(TaskId))]
    public TaskItem TaskItem { get; set; } = null!;


    public bool IsInternal { get; set; } = false;
    public bool RequiresApproval { get; set; } = false;
    public bool IsApproved { get; set; } = true;
    public DateTime? ApprovedAt { get; set; }
    public Guid? ApprovedById { get; set; }

    [ForeignKey(nameof(ApprovedById))]
    public User? ApprovedBy { get; set; }

    public int LikeCount { get; set; } = 0;
    public ICollection<CommentReaction> Reactions { get; set; } = new List<CommentReaction>();

    [StringLength(20)]
    public string? Status { get; set; } = "Active";

    [StringLength(50)]
    public string? CommentType { get; set; } = "General";

    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }


    [NotMapped]
    public bool HasReplies => Replies?.Any() == true;


    [NotMapped]
    public bool IsThreadRoot => ParentId == null;

    [NotMapped]
    public int TotalReplyCount => GetTotalReplyCount();


    [NotMapped]
    public string DisplayMessage => IsDeleted ? "[Comment deleted]" : Message;


    private int GetTotalReplyCount()
    {
        if (Replies == null || !Replies.Any()) return 0;

        return Replies.Count + Replies.Sum(r => r.GetTotalReplyCount());
    }

    public void SoftDelete(Guid deleterId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedById = deleterId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Pin(Guid pinnerId)
    {
        IsPinned = true;
        PinnedAt = DateTime.UtcNow;
        PinnedById = pinnerId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Unpin()
    {
        IsPinned = false;
        PinnedAt = null;
        PinnedById = null;
        UpdatedAt = DateTime.UtcNow;
    }

    // Index configuration
    public static void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.TaskId)
            .HasDatabaseName("IX_Comment_TaskId");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.CommenterId)
            .HasDatabaseName("IX_Comment_CommenterId");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.ParentId)
            .HasDatabaseName("IX_Comment_ParentId");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.CreatedAt)
            .HasDatabaseName("IX_Comment_CreatedAt");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => new { c.TaskId, c.CreatedAt })
            .HasDatabaseName("IX_Comment_TaskId_CreatedAt");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.IsDeleted)
            .HasDatabaseName("IX_Comment_IsDeleted");

        modelBuilder.Entity<Comment>()
            .HasIndex(c => c.IsPinned)
            .HasDatabaseName("IX_Comment_IsPinned");
    }

    // Model configuration
    public static void ConfigureRelations(ModelBuilder modelBuilder)
    {
        // Self-referencing relationship for thread hierarchy
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Parent)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Commenter relationship
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Commenter)
            .WithMany()
            .HasForeignKey(c => c.CommenterId)
            .OnDelete(DeleteBehavior.Restrict);

        // Task relationship
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.TaskItem)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
