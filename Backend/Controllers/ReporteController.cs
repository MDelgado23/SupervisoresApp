using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Models;
using Backend.Services.Validations;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReportesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetAll()
    {
        var reportes = await _context.Reportes
            .Where(r => r.Active)
            .ToListAsync();

        return Ok(reportes);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetById(int id)
    {
        var reporte = await _context.Reportes
            .Include(r => r.DetalleReportes)
            .FirstOrDefaultAsync(r => r.Id == id && r.Active);

        if (reporte == null)
            return NotFound();

        return Ok(reporte);
    }

    [HttpGet("ids-usuario/{usuarioId}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetReporteIdsYFechasByUsuario(string usuarioId, [FromQuery] int dias = 31)
    {
        var fechaLimite = DateTime.Today.AddDays(-dias);

        var reportes = await _context.Reportes
           .Where(r => r.UsuarioId == usuarioId && r.Active && r.Fecha >= fechaLimite)
           .OrderByDescending(r => r.Fecha)
           .Select(r => new
           {
               r.Id,
               r.Fecha,
               r.Finalizado
           })
           .ToListAsync();

        return Ok(reportes);
    }

    [HttpPost]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> Create([FromBody] Reporte reporte)
    {
        _context.Reportes.Add(reporte);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = reporte.Id }, reporte);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> Update(int id, [FromBody] Reporte updated)
    {
        var reporte = await _context.Reportes
            .Include(r => r.DetalleReportes)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (reporte == null || !reporte.Active)
            return NotFound();

        if (updated.Finalizado)
        {
            var validacion = ReporteValidator.ValidarReporteFinalizado(updated);
            if (validacion != null)
                return BadRequest(validacion);
        }

        // Actualizar campos base
        reporte.UsuarioId = updated.UsuarioId;
        reporte.VehiculoId = updated.VehiculoId;
        reporte.HoraInicio = updated.HoraInicio;
        reporte.HoraFin = updated.HoraFin;
        reporte.KmInicio = updated.KmInicio;
        reporte.KmFin = updated.KmFin;
        reporte.Fecha = updated.Fecha;
        reporte.HsTotales = updated.HsTotales;
        reporte.HsTranslados = updated.HsTranslados;
        reporte.HsSupervision = updated.HsSupervision;
        reporte.HsAdministrativas = updated.HsAdministrativas;
        reporte.HsApoyo = updated.HsApoyo;
        reporte.Finalizado = updated.Finalizado;
        reporte.Observaciones = updated.Observaciones;
        Console.WriteLine($"HoraFin recibida: {updated.HoraFin}");

        // Reemplazar detalles si se envían nuevos
        if (updated.DetalleReportes != null)
        {
            if (reporte.DetalleReportes?.Any() == true)
                _context.ReportesDetalles.RemoveRange(reporte.DetalleReportes);

            foreach (var nuevoDetalle in updated.DetalleReportes)
            {
                nuevoDetalle.ReporteId = reporte.Id;
                _context.ReportesDetalles.Add(nuevoDetalle);
            }
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }



    [HttpDelete("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Delete(int id)
    {
        var reporte = await _context.Reportes.FindAsync(id);
        if (reporte == null || !reporte.Active)
            return NotFound();

        reporte.Active = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
