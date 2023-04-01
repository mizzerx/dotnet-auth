using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServiceApi.Models;

[Table("roles")]
public class Roles : BaseModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public virtual ICollection<Permissions> Permissions { get; set; } = null!;

    public virtual ICollection<Users> Users { get; set; } = null!;
}