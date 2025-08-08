public class ReporteDetalleDto
{
    public int? ObjetivoId { get; set; }
    public int? VigiladorId { get; set; }
    public int? ActividadId { get; set; }
    public string? ActividadDescripcion { get; set; }
    public TimeSpan HoraEntrada { get; set; }
    public TimeSpan? HoraSalida { get; set; }

    public int? Horario { get; set; }
    public int? Presencia { get; set; }
    public int? Elementos { get; set; }
    public int? Documentos { get; set; }
    public int? Atencion { get; set; }
    public int? Puestos { get; set; }
}
