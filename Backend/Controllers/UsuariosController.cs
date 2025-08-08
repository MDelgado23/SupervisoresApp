using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
[Authorize]
public async Task<IActionResult> GetCurrentUser()
{
    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    if (string.IsNullOrEmpty(userId))
        return Unauthorized("ID de usuario no encontrado");

    var usuario = await _context.Users
        .Where(u => u.Id == userId && u.Active)
        .Select(u => new
        {
            u.Id,
            u.FullName,
            u.UserName,
            u.Email,
            u.Rol
        })
        .FirstOrDefaultAsync();

    if (usuario == null)
        return Unauthorized();

    return Ok(usuario);
}
}
