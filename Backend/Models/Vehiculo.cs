using Backend.Models;

namespace Backend.Models;

public class Vehiculo : BaseEntity
{
    public int Id { get; set; }
    public required string Patente { get; set; }
    public required string Marca { get; set; }
    public required string Modelo { get; set; }
    public int Km { get; set; }
}