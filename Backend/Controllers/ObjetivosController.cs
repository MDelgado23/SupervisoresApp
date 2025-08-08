using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ObjetivosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ObjetivosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetAll()
    {
        var objetivos = await _context.Objetivos
            .Where(o => o.Active)
            .ToListAsync();

        return Ok(objetivos);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "12,13,23")]
    public async Task<IActionResult> GetById(int id)
    {
        var objetivo = await _context.Objetivos.FindAsync(id);
        if (objetivo == null || !objetivo.Active)
            return NotFound();

        return Ok(objetivo);
    }

    [HttpPost]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Create([FromBody] Objetivo objetivo)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Objetivos.Add(objetivo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = objetivo.Id }, objetivo);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Update(int id, [FromBody] Objetivo updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var objetivo = await _context.Objetivos.FindAsync(id);
        if (objetivo == null || !objetivo.Active)
            return NotFound();

        objetivo.Name = updated.Name;
        objetivo.Location = updated.Location;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "23")]
    public async Task<IActionResult> Delete(int id)
    {
        var objetivo = await _context.Objetivos.FindAsync(id);
        if (objetivo == null || !objetivo.Active)
            return NotFound();

        objetivo.Active = false;
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
