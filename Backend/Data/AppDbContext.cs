using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using System.Globalization;

namespace Backend.Data;

public class AppDbContext : IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Objetivo> Objetivos { get; set; }
    public DbSet<Actividad> Actividades { get; set; }
    public DbSet<Vigilador> Vigiladores { get; set; }
    public DbSet<Reporte> Reportes { get; set; }
    public DbSet<ReporteDetalle> ReportesDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Usuario → Reportes (1:N)
        builder.Entity<Reporte>()
            .HasOne<Usuario>()
            .WithMany()
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        // Vehiculo → Reportes (1:N)
        builder.Entity<Reporte>()
            .HasOne<Vehiculo>()
            .WithMany()
            .HasForeignKey(r => r.VehiculoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Reporte → ReporteDetalle (1:N)
        builder.Entity<ReporteDetalle>()
            .HasOne(d => d.Reporte)
            .WithMany(r => r.DetalleReportes)
            .HasForeignKey(d => d.ReporteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Objetivo → ReporteDetalle (1:N)
        builder.Entity<ReporteDetalle>()
            .HasOne<Objetivo>()
            .WithMany()
            .HasForeignKey(d => d.ObjetivoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Vigilador → ReporteDetalle (1:N)
        builder.Entity<ReporteDetalle>()
            .HasOne<Vigilador>()
            .WithMany()
            .HasForeignKey(d => d.VigiladorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Actividad → ReporteDetalle (1:N)
        builder.Entity<ReporteDetalle>()
            .HasOne<Actividad>()
            .WithMany()
            .HasForeignKey(d => d.ActividadId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private DateTime GetArgentinaNow()
    {
        TimeZoneInfo argentinaTimeZone;
        try
        {
            argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Argentina/Buenos_Aires"); // Linux
        }
        catch (TimeZoneNotFoundException)
        {
            argentinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time"); // Windows
        }

        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaTimeZone);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity entidad)
            {
                var now = GetArgentinaNow();
                if (entry.State == EntityState.Added)
                    entidad.CreatedAt = now;
                entidad.UpdatedAt = now;
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity entidad)
            {
                var now = GetArgentinaNow();
                if (entry.State == EntityState.Added)
                    entidad.CreatedAt = now;

                entidad.UpdatedAt = now;
            }
        }
        Console.WriteLine($"Fecha local Argentina: {GetArgentinaNow()}");
        return await base.SaveChangesAsync(cancellationToken);
    }

    
}
