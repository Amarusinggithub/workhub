using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class EntityPropertyChange
{
    [Key]
    public int Id { get; set; }
    public string? NewValue { get; set; }
    public string? OriginalValue { get; set; }
    public string? PropertyName { get; set; }
    public string? PropertyTypeFullName { get; set; }
    public Guid AuditLogId { get; set; }
}
