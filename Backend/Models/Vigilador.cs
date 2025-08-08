using Backend.Models;

namespace Backend.Models;

public class Vigilador : BaseEntity
{
    public int Id { get; set; }
    public required string FullName { get; set; }
}
