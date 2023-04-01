using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServiceApi.Models;

[Table("permissions")]
public class Permissions : BaseModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    [NotMapped]
    public List<string> Actions { get; set; } = new List<string>();

    [NotMapped]
    public List<string> Resources { get; set; } = new List<string>();

    public virtual ICollection<Roles> Roles { get; set; } = null!;
}