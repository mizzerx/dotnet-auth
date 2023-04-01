namespace AuthServiceApi.Models;

public class BaseModel
{
    public long Id { get; set; } = -1;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}