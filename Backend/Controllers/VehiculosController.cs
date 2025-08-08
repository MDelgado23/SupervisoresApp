using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiculosController : ControllerBase
{
    private readonly AppDbContext _context;

    public VehiculosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetAll()
    {
        var vehiculos = await _context.Vehiculos
            .Where(v => v.Active)
            .ToListAsync();

        return Ok(vehiculos);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetById(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo == null || !vehiculo.Active)
            return NotFound();

        return Ok(vehiculo);
    }

    [HttpPost]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Create([FromBody] Vehiculo vehiculo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Vehiculos.Add(vehiculo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = vehiculo.Id }, vehiculo);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Update(int id, [FromBody] Vehiculo updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo == null || !vehiculo.Active)
            return NotFound();

        vehiculo.Patente = updated.Patente;
        vehiculo.Marca = updated.Marca;
        vehiculo.Modelo = updated.Modelo;
        vehiculo.Km = updated.Km;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Delete(int id)
    {
        var vehiculo = await _context.Vehiculos.FindAsync(id);
        if (vehiculo == null || !vehiculo.Active)
            return NotFound();

        vehiculo.Active = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
