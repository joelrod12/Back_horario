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
        public DbSet<Materia> Materias { get; set; }=null!;
        public DbSet<Usuario_Materia> Usuario_Materias { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la base de datos
            modelBuilder.Ignore<EmailSettings>(); // ✅ Esto es suficiente
            base.OnModelCreating(modelBuilder);
            // Semilla de datos para la tabla de grupos
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Admin", CreatedAt = new DateTime(2025, 04, 19), UpdatedAt = new DateTime(2025, 04, 19) },
                new Rol { Id = 2, Nombre = "User", CreatedAt = new DateTime(2025, 04, 19), UpdatedAt = new DateTime(2025, 04, 19) }
            );

            // Seed Usuarios
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Correo = "juan.perez@admin.com",
                    Contraseña = "$2a$11$HZ2xOG06SmGzslGxKpOk6e28TzqV7gf7q7iQ1Zq0BsjvNhcqQJBIW", // admin123
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
                    Contraseña = "$2a$11$G8raXnSkcGGQcjqhC/Ypj.LVTBwEPUEc71z/O2oM1P2qApuE6N9My", // usuario123
                    RolId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Usuario
                {
                    Id = 3,
                    Nombre = "Joel",
                    Apellido = "Rodriguez",
                    Correo = "joelrod128@gmail.com",
                    Contraseña = "$2a$11$G8raXnSkcGGQcjqhC/Ypj.LVTBwEPUEc71z/O2oM1P2qApuE6N9My",
                    RolId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Materias
            modelBuilder.Entity<Materia>().HasData(
                new Materia
                {
                    Id = 1,
                    Nombre = "Álgebra Lineal",
                    Color = "#9B59B6",
                    Semestre = "2",
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Materia
                {
                    Id = 2,
                    Nombre = "Programación Avanzada",
                    Color = "#3498DB",
                    Semestre = "2",
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Usuario_Materia
            modelBuilder.Entity<Usuario_Materia>().HasData(
                new Usuario_Materia
                {
                    Id = 1,
                    UsuarioId = 3,  // Joel
                    MateriaId = 1,  // Álgebra Lineal
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Usuario_Materia
                {
                    Id = 2,
                    UsuarioId = 1,  // Juan
                    MateriaId = 2,  // Programación Avanzada
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Usuario_Materia
                {
                    Id = 3,
                    UsuarioId = 2,  // Ana
                    MateriaId = 2,  // Programación Avanzada
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Grupo
            modelBuilder.Entity<Grupo>().HasData(
                new Grupo
                {
                    Id = 1,
                    Nombre = "Grupo A",
                    Color = "#FF5733",
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Temas
            modelBuilder.Entity<Tema>().HasData(
                new Tema
                {
                    Id = 1,
                    Nombre = "1.1 Álgebra",
                    Color = "#FF5733",
                    Unidad = "2",
                    MateriaId = 1,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Tema
                {
                    Id = 2,
                    Nombre = "1.2 Conceptos de Programación",
                    Color = "#33A1FF",
                    Unidad = "2",
                    MateriaId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Tema
                {
                    Id = 3,
                    Nombre = "1.3 Diagrama de Flujo",
                    Color = "#FF33A1",
                    Unidad = "2",
                    MateriaId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Actividades
            modelBuilder.Entity<Actividad>().HasData(
                new Actividad
                {
                    Id = 1,
                    Descripcion = "Resolver problemas de matrices",
                    TemaId = 1,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Actividad
                {
                    Id = 2,
                    Descripcion = "Desarrollar aplicación de consola en C#",
                    TemaId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Actividad
                {
                    Id = 3,
                    Descripcion = "Presentación sobre principios SOLID",
                    TemaId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                }
            );

            // Seed Horarios
            modelBuilder.Entity<Horario>().HasData(
                new Horario
                {
                    Id = 1,
                    Fecha = new DateTime(2025, 05, 21, 9, 0, 0),
                    FechaFin = new DateTime(2025, 05, 21, 11, 0, 0),
                    Descripcion = "Clase de introducción al álgebra",
                    Tarea = "Ejercicios 1-10 del capítulo 1",
                    Salon = "101",
                    Edificio = "Edificio Principal",
                    GrupoId = 1,
                    Usuario_MateriaId = 1,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Horario
                {
                    Id = 2,
                    Fecha = new DateTime(2025, 05, 22, 14, 0, 0),
                    FechaFin = new DateTime(2025, 05, 22, 16, 0, 0),
                    Descripcion = "Sesión de programación orientada a objetos",
                    Tarea = "Desarrollar ejercicio de herencia",
                    Salon = "Lab 3",
                    Edificio = "Edificio de Computación",
                    GrupoId = 1,
                    Usuario_MateriaId = 2,
                    CreatedAt = new DateTime(2025, 04, 19),
                    UpdatedAt = new DateTime(2025, 04, 19)
                },
                new Horario
                {
                    Id = 3,
                    Fecha = new DateTime(2025, 05, 23, 10, 0, 0),
                    FechaFin = new DateTime(2025, 05, 23, 12, 0, 0),
                    Descripcion = "Práctica de patrones de diseño",
                    Tarea = "Implementar Factory Method",
                    Salon = "Lab 2",
                    Edificio = "Edificio de Computación",
                    GrupoId = 1,
                    Usuario_MateriaId = 3,
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
            modelBuilder.Entity<Materia>().ToTable("Materias");
            modelBuilder.Entity<Usuario_Materia>().ToTable("Usuario_Materias");
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
            modelBuilder.Entity<Materia>().Property(m => m.Nombre).IsRequired().HasMaxLength(200);


            // Relaciones
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);

            modelBuilder.Entity<Tema>()
                .HasOne(t => t.Materias)
                .WithMany(m => m.Temas)
                .HasForeignKey(t => t.MateriaId);

            modelBuilder.Entity<Usuario>()
               .HasMany(u => u.Usuario_Materias)
               .WithOne(um => um.Usuarios)
               .HasForeignKey(um => um.UsuarioId);

            modelBuilder.Entity<Materia>()
               .HasMany(m => m.Usuario_Materias)
               .WithOne(um => um.Materias)
               .HasForeignKey(um => um.MateriaId);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Grupos)
                .WithMany(g => g.Horarios)
                .HasForeignKey(h => h.GrupoId);

            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Usuario_Materias)
                .WithMany(m => m.Horarios)
                .HasForeignKey(h => h.Usuario_MateriaId);

            modelBuilder.Entity<Actividad>()
              .HasOne(a => a.Temas)
              .WithMany(t => t.Actividades)
              .HasForeignKey(a => a.TemaId)
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
            modelBuilder.Entity<Materia>()
                .Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Materia>()
                .Property(m => m.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
