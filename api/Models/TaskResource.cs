using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;
public class TaskResource
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ResourceId { get; set; }
    public Resource Resource { get; set; }

    [Required]
    public int TaskId { get; set; }
    public Task Task { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastDownloadAt { get; set; }
    public DateTime LastOpenAt { get; set; }




}
