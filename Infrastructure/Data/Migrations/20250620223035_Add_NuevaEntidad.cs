using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Mediconnet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_NuevaEntidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "NEstadoUsuarioFK",
                table: "tUsuarios");

            migrationBuilder.DropIndex(
                name: "NRolFK",
                table: "tUsuarios");

            migrationBuilder.CreateIndex(
                name: "IX_tUsuarios_NEstadoUsuarioFK",
                table: "tUsuarios",
                column: "NEstadoUsuarioFK");

            migrationBuilder.CreateIndex(
                name: "IX_tUsuarios_NRolFK",
                table: "tUsuarios",
                column: "NRolFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_EstadoUsuarios",
                table: "tUsuarios",
                column: "NEstadoUsuarioFK",
                principalTable: "TEstadoUsuario",
                principalColumn: "NEstadoUsuarioID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Rol",
                table: "tUsuarios",
                column: "NRolFK",
                principalTable: "TRol",
                principalColumn: "NRolID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_EstadoUsuarios",
                table: "tUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Rol",
                table: "tUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_tUsuarios_NEstadoUsuarioFK",
                table: "tUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_tUsuarios_NRolFK",
                table: "tUsuarios");

            migrationBuilder.CreateIndex(
                name: "NEstadoUsuarioFK",
                table: "tUsuarios",
                column: "NEstadoUsuarioFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "NRolFK",
                table: "tUsuarios",
                column: "NRolFK",
                unique: true);
        }
    }
}
