namespace Backend.Models;

public abstract class BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool Active { get; set; } = true;
}
