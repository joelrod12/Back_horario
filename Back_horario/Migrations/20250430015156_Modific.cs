using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_horario.Migrations
{
    /// <inheritdoc />
    public partial class Modific : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horarios_Usuarios_UsuarioId",
                table: "Horarios");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Temas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TemaDTO",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Temas",
                keyColumn: "Id",
                keyValue: 1,
                column: "UsuarioId",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Temas_UsuarioId",
                table: "Temas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Horarios_Usuarios_UsuarioId",
                table: "Horarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Temas_Usuarios_UsuarioId",
                table: "Temas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Horarios_Usuarios_UsuarioId",
                table: "Horarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Temas_Usuarios_UsuarioId",
                table: "Temas");

            migrationBuilder.DropIndex(
                name: "IX_Temas_UsuarioId",
                table: "Temas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Temas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TemaDTO");

            migrationBuilder.AddForeignKey(
                name: "FK_Horarios_Usuarios_UsuarioId",
                table: "Horarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
