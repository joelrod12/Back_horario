using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_horario.Migrations
{
    /// <inheritdoc />
    public partial class Mejoras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materias_Temas_TemaId",
                table: "Materias");

            migrationBuilder.DropIndex(
                name: "IX_Materias_TemaId",
                table: "Materias");

            migrationBuilder.DropColumn(
                name: "TemaId",
                table: "Materias");

            migrationBuilder.AddColumn<int>(
                name: "MateriaId",
                table: "Temas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MateriaId", "Nombre" },
                values: new object[] { 1, "1.1 Álgebra" });

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MateriaId", "Nombre" },
                values: new object[] { 2, "1.2 Conceptos de Programación" });

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MateriaId", "Nombre" },
                values: new object[] { 2, "1.3 Diagrama de Flujo" });

            migrationBuilder.UpdateData(
                table: "Usuario_Materias",
                keyColumn: "Id",
                keyValue: 3,
                column: "UsuarioId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Joel");

            migrationBuilder.CreateIndex(
                name: "IX_Temas_MateriaId",
                table: "Temas",
                column: "MateriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Temas_Materias_MateriaId",
                table: "Temas",
                column: "MateriaId",
                principalTable: "Materias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Temas_Materias_MateriaId",
                table: "Temas");

            migrationBuilder.DropIndex(
                name: "IX_Temas_MateriaId",
                table: "Temas");

            migrationBuilder.DropColumn(
                name: "MateriaId",
                table: "Temas");

            migrationBuilder.AddColumn<int>(
                name: "TemaId",
                table: "Materias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Materias",
                keyColumn: "Id",
                keyValue: 1,
                column: "TemaId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Materias",
                keyColumn: "Id",
                keyValue: 2,
                column: "TemaId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Matemáticas");

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Programación");

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Ciencias");

            migrationBuilder.UpdateData(
                table: "Usuario_Materias",
                keyColumn: "Id",
                keyValue: 3,
                column: "UsuarioId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "joel");

            migrationBuilder.CreateIndex(
                name: "IX_Materias_TemaId",
                table: "Materias",
                column: "TemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materias_Temas_TemaId",
                table: "Materias",
                column: "TemaId",
                principalTable: "Temas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
