using Backend.Models;

namespace Backend.Models;

public class Objetivo : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
}