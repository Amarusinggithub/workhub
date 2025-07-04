using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class TaskLabel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TaskId { get; set; }
    public Task Task { get; set; }


    [Required]
    public int LabelId { get; set; }
    public Label Label { get; set; }


    public DateTime AttachedAt { get; set; }


}
