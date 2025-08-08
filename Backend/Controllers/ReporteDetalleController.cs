using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Models;
using Backend.Services.Validations;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReporteDetallesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReporteDetallesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetAll()
    {
        var detalles = await _context.ReportesDetalles
            .Where(d => d.Active)
            .ToListAsync();

        return Ok(detalles);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetById(int id)
    {
        var detalle = await _context.ReportesDetalles.FindAsync(id);
        if (detalle == null || !detalle.Active)
            return NotFound();

        return Ok(detalle);
    }

    [HttpPost]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> Create([FromBody] ReporteDetalle detalle)
    {
        var reporte = await _context.Reportes
            .Include(r => r.DetalleReportes)
            .FirstOrDefaultAsync(r => r.Id == detalle.ReporteId && r.Active);

        if (reporte == null)
            return BadRequest("Reporte asociado no encontrado.");

        var error = ReporteValidator.ValidarDetalleConReporte(detalle, reporte);
        if (error != null)
            return BadRequest(error);

        _context.ReportesDetalles.Add(detalle);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = detalle.Id }, detalle);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> Update(int id, [FromBody] ReporteDetalle updated)
    {
        var detalle = await _context.ReportesDetalles.FindAsync(id);
        if (detalle == null || !detalle.Active)
            return NotFound();

        var reporte = await _context.Reportes
            .Include(r => r.DetalleReportes)
            .FirstOrDefaultAsync(r => r.Id == updated.ReporteId && r.Active);

        if (reporte == null)
            return BadRequest("Reporte asociado no encontrado.");

        updated.Id = detalle.Id; // para excluirse en comparación
        var error = ReporteValidator.ValidarDetalleConReporte(updated, reporte);
        if (error != null)
            return BadRequest(error);

        detalle.ReporteId = updated.ReporteId;
        detalle.ObjetivoId = updated.ObjetivoId;
        detalle.VigiladorId = updated.VigiladorId;
        detalle.HoraEntrada = updated.HoraEntrada;
        detalle.HoraSalida = updated.HoraSalida;
        detalle.ActividadId = updated.ActividadId;
        detalle.ActividadDescripcion = updated.ActividadDescripcion;
        detalle.Atencion = updated.Atencion;
        detalle.Documentos = updated.Documentos;
        detalle.Puestos = updated.Puestos;
        detalle.Elementos = updated.Elementos;
        detalle.Presencia = updated.Presencia;
        detalle.Horario = updated.Horario;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> Delete(int id)
    {
        var detalle = await _context.ReportesDetalles.FindAsync(id);
        if (detalle == null)
            return NotFound();

        _context.ReportesDetalles.Remove(detalle);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
