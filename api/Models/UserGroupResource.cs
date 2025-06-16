using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models;

public class UserGroupResource
{


    [Key]
    public int Id { get; set; }

    [Required]
    public int ResourceId { get; set; }
    public Resource Resource { get; set; }

    [Required]
    public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; }

    public ResorceLocation ResourceLocation { get; set; }
    public DateTime LastDownloadAt { get; set; }




}
