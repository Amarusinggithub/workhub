using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class AuditLog
{
    [Key]
    public int Id { get; set; }
    public string Action { get; set; }
    public string  UserId { get; set; }
    public User User { get; set; }

    public DateTime PerformedAt { get; set; }
    public string AffectedEntity { get; set; }
    public string? MetadataJson { get; set; }
}
