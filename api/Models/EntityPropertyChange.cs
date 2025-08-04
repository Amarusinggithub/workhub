using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models;

public class EntityPropertyChange
{
    [Key]
    public int Id { get; set; }
    public string? NewValue { get; set; }
    public string? OriginalValue { get; set; }
    public string? PropertyName { get; set; }
    public string? PropertyTypeFullName { get; set; }
    public int AuditLogId { get; set; }
    [ForeignKey(nameof(AuditLogId))]
    public AuditLog AuditLog { get; set; }


    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
}
