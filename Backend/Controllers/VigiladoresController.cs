using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VigiladoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public VigiladoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetAll()
    {
        var vigiladores = await _context.Vigiladores
            .Where(v => v.Active)
            .ToListAsync();

        return Ok(vigiladores);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetById(int id)
    {
        var vigilador = await _context.Vigiladores.FindAsync(id);
        if (vigilador == null || !vigilador.Active)
            return NotFound();

        return Ok(vigilador);
    }

    [HttpPost]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Create([FromBody] Vigilador vigilador)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Vigiladores.Add(vigilador);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = vigilador.Id }, vigilador);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Update(int id, [FromBody] Vigilador updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var vigilador = await _context.Vigiladores.FindAsync(id);
        if (vigilador == null || !vigilador.Active)
            return NotFound();

        vigilador.FullName = updated.FullName;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Delete(int id)
    {
        var vigilador = await _context.Vigiladores.FindAsync(id);
        if (vigilador == null || !vigilador.Active)
            return NotFound();

        vigilador.Active = false;
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
