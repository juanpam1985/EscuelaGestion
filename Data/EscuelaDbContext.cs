using EscuelaGestion.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaGestion.Data;

public class EscuelaDbContext : DbContext
{
    public EscuelaDbContext(DbContextOptions<EscuelaDbContext> options) : base(options)
    {
    }

    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
    public DbSet<Profesor> Profesores => Set<Profesor>();
    public DbSet<Clase> Clases => Set<Clase>();
    public DbSet<AsignacionClase> AsignacionesClases => Set<AsignacionClase>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.ToTable("Estudiantes");
            entity.HasKey(e => e.EstudianteId);
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(80).IsRequired();
            entity.Property(e => e.Apellido).HasColumnName("apellido").HasMaxLength(80).IsRequired();
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento").HasColumnType("date");
            entity.Property(e => e.Grado).HasColumnName("grado").HasMaxLength(30).IsRequired();
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.ToTable("Profesores");
            entity.HasKey(p => p.ProfesorId);
            entity.Property(p => p.ProfesorId).HasColumnName("profesor_id");
            entity.Property(p => p.Nombre).HasColumnName("nombre").HasMaxLength(80).IsRequired();
            entity.Property(p => p.Apellido).HasColumnName("apellido").HasMaxLength(80).IsRequired();
            entity.Property(p => p.Especialidad).HasColumnName("especialidad").HasMaxLength(80).IsRequired();
            entity.Property(p => p.Email).HasColumnName("email").HasMaxLength(120).IsRequired();
        });

        modelBuilder.Entity<Clase>(entity =>
        {
            entity.ToTable("Clases");
            entity.HasKey(c => c.ClaseId);
            entity.Property(c => c.ClaseId).HasColumnName("clase_id");
            entity.Property(c => c.Nombre).HasColumnName("nombre").HasMaxLength(100).IsRequired();
            entity.Property(c => c.Horario).HasColumnName("horario").HasMaxLength(60).IsRequired();
            entity.Property(c => c.ProfesorId).HasColumnName("profesor_id");

            entity.HasOne(c => c.Profesor)
                .WithMany(p => p.Clases)
                .HasForeignKey(c => c.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AsignacionClase>(entity =>
        {
            entity.ToTable("AsignacionesClases");
            entity.HasKey(a => a.AsignacionId);
            entity.Property(a => a.AsignacionId).HasColumnName("asignacion_id");
            entity.Property(a => a.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(a => a.ClaseId).HasColumnName("clase_id");
            entity.Property(a => a.FechaAsignacion).HasColumnName("fecha_asignacion").HasColumnType("date");

            entity.HasOne(a => a.Estudiante)
                .WithMany(e => e.Asignaciones)
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.Clase)
                .WithMany(c => c.Asignaciones)
                .HasForeignKey(a => a.ClaseId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
