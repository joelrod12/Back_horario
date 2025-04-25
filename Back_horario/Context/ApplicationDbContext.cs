using System.Diagnostics.CodeAnalysis;
using Back_horario.Models.Domain.Entities;
using Back_horario.Models.Domain.Entities.Email;
using Microsoft.EntityFrameworkCore;
using Back_horario.Models.Domain.DTO;
using BCrypt.Net;

namespace Back_horario.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Modelos que se van a pasar en la base de datos
        public DbSet<Actividad> Actividades { get; set; } = null!;
        public DbSet<Tema> Temas { get; set; } = null!;
        public DbSet<Grupo> Grupos { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Horario> Horarios { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la base de datos
            modelBuilder.Ignore<EmailSettings>(); // ✅ Esto es suficiente
            base.OnModelCreating(modelBuilder);
            // Semilla de datos para la tabla de grupos
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Correo = "juan.perez@admin.com",
                    // admin123
                    Contraseña = "$2a$11$HZ2xOG06SmGzslGxKpOk6e28TzqV7gf7q7iQ1Zq0BsjvNhcqQJBIW",
                    RolId = 1,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Usuario
                {
                    Id = 2,
                    Nombre = "Ana",
                    Apellido = "López",
                    Correo = "ana.lopez@usuario.com",
                    // usuario123
                    Contraseña = "$2a$11$G8raXnSkcGGQcjqhC/Ypj.LVTBwEPUEc71z/O2oM1P2qApuE6N9My",
                    RolId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
                );

            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    Id = 1,
                    Nombre = "Admin",
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Rol
                {
                    Id = 2,
                    Nombre = "User",
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Grupos
            modelBuilder.Entity<Grupo>().HasData(
                new Grupo
                {
                    Id = 1,
                    Nombre = "Grupo A",
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Temas
            modelBuilder.Entity<Tema>().HasData(
                new Tema
                {
                    Id = 1,
                    Nombre = "Matemáticas",
                    Color = "#FF5733",
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Horarios
            modelBuilder.Entity<Horario>().HasData(
                new Horario
                {
                    Id = 1,
                    Fecha = new DateTime(2025, 04, 21),
                    Descripcion = "Clase de repaso",
                    Tarea = "Resolver ejercicios",
                    Salon = "101",
                    Edificio = "Edificio A",
                    GrupoId = 1,
                    TemaId = 1,
                    UsuarioId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Actividades
            modelBuilder.Entity<Actividad>().HasData(
                new Actividad
                {
                    Id = 1,
                    Descripcion = "Resolver problemas de álgebra",
                    HorarioId = 1,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );
 
            // Configuración de las entidades
            modelBuilder.Entity<Rol>().ToTable("Roles");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Grupo>().ToTable("Grupos");
            modelBuilder.Entity<Tema>().ToTable("Temas");
            modelBuilder.Entity<Horario>().ToTable("Horarios");
            modelBuilder.Entity<Actividad>().ToTable("Actividades");
            // Configuración de las propiedades
            modelBuilder.Entity<Rol>().Property(r => r.Nombre).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Usuario>().Property(u => u.Nombre).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Usuario>().Property(u => u.Correo).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Usuario>().Property(u => u.Contraseña).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Usuario>().Property(u => u.RolId).IsRequired();
            modelBuilder.Entity<Grupo>().Property(g => g.Nombre).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Tema>().Property(t => t.Nombre).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Horario>().Property(h => h.Fecha).IsRequired();
            modelBuilder.Entity<Horario>().Property(h => h.Tarea).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Horario>().Property(h => h.Descripcion).HasMaxLength(200);
            modelBuilder.Entity<Horario>().Property(h => h.Salon).HasMaxLength(200);
            modelBuilder.Entity<Horario>().Property(h => h.Edificio).HasMaxLength(200);
            modelBuilder.Entity<Actividad>().Property(a => a.Descripcion).HasMaxLength(200);

            // Relaciones
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Grupos)
                .WithMany(g => g.Horarios)
                .HasForeignKey(h => h.GrupoId);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Temas)
                .WithMany(t => t.Horarios)
                .HasForeignKey(h => h.TemaId);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Usuarios)
                .WithMany(u => u.Horarios)
                .HasForeignKey(h => h.UsuarioId);

            modelBuilder.Entity<Actividad>()
              .HasOne(a => a.Horarios)
              .WithMany(h => h.Actividades)
              .HasForeignKey(a => a.HorarioId)
              .OnDelete(DeleteBehavior.Cascade);

            // Configuración de las fechas de creación y actualización
            modelBuilder.Entity<Rol>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Rol>()
                .Property(r => r.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Usuario>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Usuario>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Grupo>()
                .Property(g => g.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Grupo>()
                .Property(g => g.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Tema>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Tema>()
                .Property(t => t.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Horario>()
                .Property(h => h.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Horario>()
                .Property(h => h.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
            modelBuilder.Entity<Actividad>()
                .Property(a => a.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Actividad>()
                .Property(a => a.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
        }
        public DbSet<Back_horario.Models.Domain.DTO.TemaDTO> TemaDTO { get; set; } = default!;
    }
}
