using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class User: IdentityUser
{
   
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }


}
