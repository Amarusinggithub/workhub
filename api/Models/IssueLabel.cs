using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class IssueLabel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int IssueId { get; set; }
    public Issue Issue { get; set; }


    [Required]
    public int LabelId { get; set; }
    public Label Label { get; set; }


    public DateTime AttachedAt { get; set; }


}
