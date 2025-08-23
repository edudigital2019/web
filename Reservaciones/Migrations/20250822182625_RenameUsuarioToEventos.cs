using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservaciones.Migrations
{
    /// <inheritdoc />
    public partial class RenameUsuarioToEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservaciones_Usuarios_EventoId",
                table: "Reservaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Eventos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eventos",
                table: "Eventos",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservaciones_Eventos_EventoId",
                table: "Reservaciones",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservaciones_Eventos_EventoId",
                table: "Reservaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eventos",
                table: "Eventos");

            migrationBuilder.RenameTable(
                name: "Eventos",
                newName: "Usuarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservaciones_Usuarios_EventoId",
                table: "Reservaciones",
                column: "EventoId",
                principalTable: "Usuarios",
                principalColumn: "EventoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
