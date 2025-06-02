using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class WorkSpaceType
{
    [Key]
    public int Id { get; set; }
    public string TypeName { get; set; }
    public int MembersMin { get; set; }
    public int MembersMax { get; set; }



}
