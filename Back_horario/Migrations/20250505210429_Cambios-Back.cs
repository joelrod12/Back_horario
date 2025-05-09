using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_horario.Migrations
{
    /// <inheritdoc />
    public partial class CambiosBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unidad",
                table: "Materias");

            migrationBuilder.AddColumn<string>(
                name: "Unidad",
                table: "Temas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 1,
                column: "Unidad",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 2,
                column: "Unidad",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 3,
                column: "Unidad",
                value: "2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unidad",
                table: "Temas");

            migrationBuilder.AddColumn<string>(
                name: "Unidad",
                table: "Materias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Materias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Unidad",
                value: "2");

            migrationBuilder.UpdateData(
                table: "Materias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Unidad",
                value: "2");
        }
    }
}
