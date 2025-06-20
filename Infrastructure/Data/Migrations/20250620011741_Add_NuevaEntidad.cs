using Microsoft.EntityFrameworkCore.Metadata;
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
            migrationBuilder.RenameIndex(
                name: "CNombre1",
                table: "TRol",
                newName: "CNombre2");

            migrationBuilder.RenameIndex(
                name: "CNombre",
                table: "TEstadoVerificacion",
                newName: "CNombre1");

            migrationBuilder.CreateTable(
                name: "TEstadoUsuario",
                columns: table => new
                {
                    NEstadoUsuarioID = table.Column<int>(type: "int(12)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNombre = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NEstadoUsuarioID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "CNombre",
                table: "TEstadoUsuario",
                column: "CNombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEstadoUsuario");

            migrationBuilder.RenameIndex(
                name: "CNombre2",
                table: "TRol",
                newName: "CNombre1");

            migrationBuilder.RenameIndex(
                name: "CNombre1",
                table: "TEstadoVerificacion",
                newName: "CNombre");
        }
    }
}
