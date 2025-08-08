using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IConfiguration _config;

    public AuthController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        var user = new Usuario
        {
            UserName = model.Dni,
            FullName = model.FullName,
            Email = model.Email,
            Rol = model.Rol
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
            return Ok("Usuario registrado correctamente");

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var user = await _userManager.FindByNameAsync(model.Dni);
        if (user == null || !user.Active)
            return Unauthorized("Usuario no válido o inactivo");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Credenciales inválidas");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(Usuario user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim("fullname", user.FullName ?? ""),
            new Claim(ClaimTypes.Role, user.Rol.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
