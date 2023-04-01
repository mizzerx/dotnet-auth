using System.ComponentModel.DataAnnotations;

namespace AuthServiceApi.Models;

public class Users : BaseModel
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public virtual ICollection<Roles> Roles { get; set; } = null!;
}