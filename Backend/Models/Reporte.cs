using Backend.Models;

namespace Backend.Models;

public class Reporte : BaseEntity
{
    public int Id { get; set; }
    public required string UsuarioId { get; set; }
    public int? VehiculoId { get; set; }
    public TimeSpan? HoraInicio { get; set; }
    public TimeSpan? HoraFin { get; set; }
    public int? KmInicio { get; set; }
    public int? KmFin { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan? HsTotales { get; set; }
    public TimeSpan? HsTranslados { get; set; }
    public TimeSpan? HsSupervision { get; set; }

    public TimeSpan? HsAdministrativas { get; set; }
    public TimeSpan? HsApoyo { get; set; }
    public List<ReporteDetalle>? DetalleReportes { get; set; } = new();

    public string? Observaciones { get; set; }
    public bool Finalizado { get; set; } = false;
}
