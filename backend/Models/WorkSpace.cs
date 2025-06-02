using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class WorkSpace
{
    [Key]
    public int Id { get; set; }
    public int GroupTypeId { get; set; }
    public WorkSpaceType WorkSpaceType { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public invoice Invoice { get; set; }
}
