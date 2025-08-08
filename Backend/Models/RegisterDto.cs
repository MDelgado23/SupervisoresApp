namespace Backend.Models;

public class RegisterDto
{
    public required string Dni { get; set; }
    public required string Password { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public int Rol { get; set; }
}
