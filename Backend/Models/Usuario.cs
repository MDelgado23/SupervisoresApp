using Microsoft.AspNetCore.Identity;

namespace Backend.Models;

public class Usuario : IdentityUser
{   
    public required string FullName { get; set; }
    public int Rol { get; set; } // 12=Controlador, 13=Supervisor, 23=Gerente
    public bool Active { get; set; } = true;
}
