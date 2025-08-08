using Backend.Models;

namespace Backend.Services.Validations;

public static class ReporteValidator
{
    // 🔹 Validación completa para finalizar un reporte
    public static string? ValidarReporteFinalizado(Reporte reporte)
    {
        if (string.IsNullOrWhiteSpace(reporte.UsuarioId))
            return "UsuarioId es obligatorio.";

        if (reporte.Fecha == default)
            return "Fecha es obligatoria.";

        if (reporte.HoraInicio is null || reporte.HoraFin is null)
            return "Debe completar la hora de inicio y fin.";

        if (reporte.HoraFin < reporte.HoraInicio)
            return "La hora de finalización no puede ser anterior a la hora de inicio.";

        if (reporte.KmInicio is null || reporte.KmFin is null)
            return "Debe completar los kilómetros de inicio y fin.";

        if (reporte.KmFin < reporte.KmInicio)
            return "Los kilómetros finales no pueden ser menores a los iniciales.";

        if (reporte.DetalleReportes is null || !reporte.DetalleReportes.Any())
            return "El reporte debe tener al menos un detalle.";

        for (int i = 0; i < reporte.DetalleReportes.Count; i++)
        {
            var detalle = reporte.DetalleReportes[i];

            if (detalle.ObjetivoId is null) return $"El detalle #{i + 1} debe tener un objetivo.";
            if (detalle.VigiladorId is null) return $"El detalle #{i + 1} debe tener un vigilador.";
            if (detalle.ActividadId is null) return $"El detalle #{i + 1} debe tener una actividad.";
            if (detalle.HoraEntrada is null || detalle.HoraSalida is null)
                return $"Debe completar la hora de entrada y salida en el detalle #{i + 1}.";

            if (detalle.HoraSalida < detalle.HoraEntrada)
                return $"La hora de salida no puede ser anterior a la entrada en el detalle #{i + 1}.";

            if (i > 0)
            {
                var anterior = reporte.DetalleReportes[i - 1];
                if (detalle.HoraEntrada < anterior.HoraSalida)
                    return $"La hora de entrada en el detalle #{i + 1} no puede ser anterior a la salida del detalle anterior.";
            }

            var errorCalif = ValidarTodasLasCalificaciones(detalle, i + 1);
            if (errorCalif != null) return errorCalif;
        }

        var ultimaSalida = reporte.DetalleReportes.Max(d => d.HoraSalida);
        if (reporte.HoraFin < ultimaSalida)
            return "La hora de finalización no puede ser anterior a la última hora de salida.";

        var totalKm = reporte.KmFin - reporte.KmInicio;
        if (totalKm < 0)
            return "El total de kilómetros no puede ser negativo.";

        return null;
    }

    // 🔹 Validación simple para un detalle aislado
    public static string? ValidarDetalleIndividual(ReporteDetalle detalle)
    {
        if (detalle.ObjetivoId is null)
            return "El objetivo es obligatorio.";

        if (detalle.VigiladorId is null)
            return "El vigilador es obligatorio.";

        if (detalle.ActividadId is null)
            return "La actividad es obligatoria.";

        if (detalle.HoraEntrada is null)
            return "La hora de entrada es obligatoria.";

        if (detalle.HoraSalida is null)
            return "La hora de salida es obligatoria.";

        if (detalle.HoraSalida < detalle.HoraEntrada)
            return "La hora de salida no puede ser anterior a la entrada.";

        return ValidarTodasLasCalificaciones(detalle);
    }

    // 🔹 Validación completa con acceso al reporte (para detectar solapamientos, etc.)
    public static string? ValidarDetalleConReporte(ReporteDetalle detalle, Reporte reporte)
    {
        var errorBasico = ValidarDetalleIndividual(detalle);
        if (errorBasico != null)
            return errorBasico;

        if (reporte.HoraInicio is not null && detalle.HoraEntrada < reporte.HoraInicio)
            return "La hora de entrada no puede ser anterior a la hora de inicio del reporte.";

        foreach (var otro in reporte.DetalleReportes)
        {
            if (otro.Id == detalle.Id) continue; // se ignora a sí mismo (PUT)

            if (detalle.HoraEntrada < otro.HoraSalida && detalle.HoraSalida > otro.HoraEntrada)
                return "El horario del detalle se superpone con otro detalle ya registrado.";
        }

        return null;
    }

    // 🔸 Valida que cada calificación esté en rango (0-5)
    private static string? ValidarTodasLasCalificaciones(ReporteDetalle detalle, int? index = null)
    {
        if (!ValidarCalificacion(detalle.Horario))
            return Mensaje("Horario");
        if (!ValidarCalificacion(detalle.Presencia))
            return Mensaje("Presencia");
        if (!ValidarCalificacion(detalle.Elementos))
            return Mensaje("Elementos");
        if (!ValidarCalificacion(detalle.Documentos))
            return Mensaje("Documentos");
        if (!ValidarCalificacion(detalle.Atencion))
            return Mensaje("Atención");
        if (!ValidarCalificacion(detalle.Puestos))
            return Mensaje("Puestos");

        return null;

        string Mensaje(string campo)
        {
            return index is null
                ? $"Calificación '{campo}' inválida."
                : $"Calificación '{campo}' inválida en el detalle #{index}.";
        }
    }

    private static bool ValidarCalificacion(int? calif)
    {
        return calif is >= 0 and <= 5;
    }
}
