using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_horario.Migrations
{
    /// <inheritdoc />
    public partial class SeedFixedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemaDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemaDTO", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Grupos",
                columns: new[] { "Id", "CreatedAt", "Nombre" },
                values: new object[] { 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grupo A" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "User");

            migrationBuilder.InsertData(
                table: "Temas",
                columns: new[] { "Id", "Color", "CreatedAt", "Nombre" },
                values: new object[] { 1, "#FF5733", new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Matemáticas" });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Contraseña",
                value: "$2a$11$HZ2xOG06SmGzslGxKpOk6e28TzqV7gf7q7iQ1Zq0BsjvNhcqQJBIW");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "Contraseña",
                value: "$2a$11$G8raXnSkcGGQcjqhC/Ypj.LVTBwEPUEc71z/O2oM1P2qApuE6N9My");

            migrationBuilder.InsertData(
                table: "Horarios",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "Edificio", "Fecha", "GrupoId", "Salon", "Tarea", "TemaId", "UsuarioId" },
                values: new object[] { 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clase de repaso", "Edificio A", new DateTime(2025, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "101", "Resolver ejercicios", 1, 2 });

            migrationBuilder.InsertData(
                table: "Actividades",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "HorarioId" },
                values: new object[] { 1, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resolver problemas de álgebra", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemaDTO");

            migrationBuilder.DeleteData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Horarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grupos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Usuario");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "Contraseña",
                value: "admin123");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                column: "Contraseña",
                value: "usuario123");
        }
    }
}
