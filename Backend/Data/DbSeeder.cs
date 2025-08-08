using Microsoft.AspNetCore.Identity;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(UserManager<Usuario> userManager, AppDbContext context)
    {
        // 📌 Usuarios
        var usuarios = new List<(string dni, string nombre, string email, int rol)>
        {
            ("99999999", "Gonzalo Gerente", "gerente@mail.com", 23),
            ("88888888", "Sofía Supervisor", "supervisor@mail.com", 13),
            ("77777777", "Carlos Controlador", "controlador@mail.com", 12)
        };

        foreach (var (dni, nombre, email, rol) in usuarios)
        {
            if (await userManager.FindByNameAsync(dni) is null)
            {
                var user = new Usuario
                {
                    UserName = dni,
                    FullName = nombre,
                    Email = email,
                    Rol = rol,
                    Active = true
                };

                var result = await userManager.CreateAsync(user, dni); // Contraseña = DNI

                if (!result.Succeeded)
                {
                    Console.WriteLine($"❌ Error creando usuario {nombre}:");
                    foreach (var error in result.Errors)
                        Console.WriteLine($"- {error.Code}: {error.Description}");
                }
                else
                {
                    Console.WriteLine($"✅ Usuario {nombre} creado correctamente.");
                }
            }
        }

        // 🚗 Vehículos
        if (!await context.Vehiculos.AnyAsync())
        {
            context.Vehiculos.AddRange(
                new Vehiculo { Patente = "ABC123", Marca = "Toyota", Modelo = "Corolla" },
                new Vehiculo { Patente = "DEF456", Marca = "Toyota", Modelo = "Corolla"  },
                new Vehiculo { Patente = "GHI789", Marca = "Toyota", Modelo = "Corolla"  }
            );
            Console.WriteLine("✅ Vehículos agregados.");
        }

        // 🎯 Objetivos
        if (!await context.Objetivos.AnyAsync())
        {
            context.Objetivos.AddRange(
                new Objetivo { Name = "Banco Nación - Centro",
    Location = "Av. Siempre Viva 123"  },
                new Objetivo { Name = "Hospital Municipal",
    Location = "Av. Siempre Viva 123" },
                new Objetivo { Name = "Escuela Técnica N°1",
    Location = "Av. Siempre Viva 123" },
                new Objetivo { Name = "Edificio Libertador",
    Location = "Av. Siempre Viva 123" }
            );
            Console.WriteLine("✅ Objetivos agregados.");
        }

        // 👮 Vigiladores
        if (!await context.Vigiladores.AnyAsync())
        {
            context.Vigiladores.AddRange(
                new Vigilador { FullName = "Lucía Fernández" },
                new Vigilador { FullName = "Esteban Ríos" },
                new Vigilador { FullName = "María López" }
            );
            Console.WriteLine("✅ Vigiladores agregados.");
        }

        // ⚙️ Actividades
        if (!await context.Actividades.AnyAsync())
        {
            context.Actividades.AddRange(
                new Actividad { Nombre = "Supervisión" },
                new Actividad { Nombre = "Administrativa" },
                new Actividad { Nombre = "Apoyo" },
                new Actividad { Nombre = "Relevamiento" }
            );
            Console.WriteLine("✅ Actividades agregadas.");
        }

        await context.SaveChangesAsync();
    }
}
