using Backend.Models;

namespace Backend.Models;

public class Actividad : BaseEntity
{
    public int Id { get; set; }
    public int Tipo { get; set; }  // 0=Apoyo, 1=Supervision, 2=Administrativa
    public required string Nombre { get; set; }
}
