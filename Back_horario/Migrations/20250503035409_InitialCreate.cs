using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Back_horario.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TemaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actividades_Temas_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Temas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Semestre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materias_Temas_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Temas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_Materias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Materias_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuario_Materias_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Tarea = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Salon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Edificio = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GrupoId = table.Column<int>(type: "int", nullable: false),
                    Usuario_MateriaId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horarios_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Horarios_Usuario_Materias_Usuario_MateriaId",
                        column: x => x.Usuario_MateriaId,
                        principalTable: "Usuario_Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Grupos",
                columns: new[] { "Id", "Color", "CreatedAt", "Nombre" },
                values: new object[] { 1, "#FF5733", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grupo A" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { 2, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" }
                });

            migrationBuilder.InsertData(
                table: "Temas",
                columns: new[] { "Id", "Color", "CreatedAt", "Nombre" },
                values: new object[,]
                {
                    { 1, "#FF5733", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Matemáticas" },
                    { 2, "#33A1FF", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programación" },
                    { 3, "#FF33A1", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ciencias" }
                });

            migrationBuilder.InsertData(
                table: "Actividades",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "TemaId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resolver problemas de matrices", 1 },
                    { 2, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desarrollar aplicación de consola en C#", 2 },
                    { 3, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Presentación sobre principios SOLID", 2 }
                });

            migrationBuilder.InsertData(
                table: "Materias",
                columns: new[] { "Id", "Color", "CreatedAt", "Nombre", "Semestre", "TemaId", "Unidad" },
                values: new object[,]
                {
                    { 1, "#9B59B6", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Álgebra Lineal", "2", 1, "2" },
                    { 2, "#3498DB", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Programación Avanzada", "2", 2, "2" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Contraseña", "Correo", "CreatedAt", "Nombre", "ResetToken", "ResetTokenExpiration", "RolId" },
                values: new object[,]
                {
                    { 1, "Pérez", "$2a$11$HZ2xOG06SmGzslGxKpOk6e28TzqV7gf7q7iQ1Zq0BsjvNhcqQJBIW", "juan.perez@admin.com", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan", null, null, 1 },
                    { 2, "López", "$2a$11$G8raXnSkcGGQcjqhC/Ypj.LVTBwEPUEc71z/O2oM1P2qApuE6N9My", "ana.lopez@usuario.com", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ana", null, null, 2 },
                    { 3, "Rodriguez", "$2a$11$G8raXnSkcGGQcjqhC/Ypj.LVTBwEPUEc71z/O2oM1P2qApuE6N9My", "joelrod128@gmail.com", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "joel", null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Usuario_Materias",
                columns: new[] { "Id", "CreatedAt", "MateriaId", "UpdatedAt", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 2, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "Horarios",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "Edificio", "Fecha", "FechaFin", "GrupoId", "Salon", "Tarea", "Usuario_MateriaId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clase de introducción al álgebra", "Edificio Principal", new DateTime(2025, 5, 21, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 21, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, "101", "Ejercicios 1-10 del capítulo 1", 1 },
                    { 2, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sesión de programación orientada a objetos", "Edificio de Computación", new DateTime(2025, 5, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 22, 16, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lab 3", "Desarrollar ejercicio de herencia", 2 },
                    { 3, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Práctica de patrones de diseño", "Edificio de Computación", new DateTime(2025, 5, 23, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, "Lab 2", "Implementar Factory Method", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_TemaId",
                table: "Actividades",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_GrupoId",
                table: "Horarios",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Horarios_Usuario_MateriaId",
                table: "Horarios",
                column: "Usuario_MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_TemaId",
                table: "Materias",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Materias_MateriaId",
                table: "Usuario_Materias",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Materias_UsuarioId",
                table: "Usuario_Materias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Usuario_Materias");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Temas");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
