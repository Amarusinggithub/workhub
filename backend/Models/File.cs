using System.ComponentModel.DataAnnotations;
using Type = backend.Enums.ItemType;
namespace backend.Models;

public class File:Item
{

    public  long FileSize { get; set; }

}
