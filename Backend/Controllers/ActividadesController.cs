using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; // ✅ Faltaba este using
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActividadesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActividadesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetAll()
    {
        var actividades = await _context.Actividades
            .Where(a => a.Active)
            .ToListAsync();

        return Ok(actividades);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetById(int id)
    {
        var actividad = await _context.Actividades.FindAsync(id);
        if (actividad == null || !actividad.Active)
            return NotFound();

        return Ok(actividad);
    }

    [HttpPost]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Create([FromBody] Actividad actividad)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Actividades.Add(actividad);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = actividad.Id }, actividad);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Update(int id, [FromBody] Actividad updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var actividad = await _context.Actividades.FindAsync(id);
        if (actividad == null || !actividad.Active)
            return NotFound();

        actividad.Nombre = updated.Nombre;
        actividad.Tipo = updated.Tipo;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Delete(int id)
    {
        var actividad = await _context.Actividades.FindAsync(id);
        if (actividad == null || !actividad.Active)
            return NotFound();

        actividad.Active = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
