using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api_Mediconnet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "TEstadoVerificacion",
                columns: table => new
                {
                    NEstadoVerificacion = table.Column<int>(type: "int(12)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNombre = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NEstadoVerificacion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TRol",
                columns: table => new
                {
                    NRolID = table.Column<int>(type: "int(32)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNombre = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NRolID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TTipoIdentificacion",
                columns: table => new
                {
                    NTipoIdentificacion = table.Column<int>(type: "int(12)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNombre = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NTipoIdentificacion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tUsuarios",
                columns: table => new
                {
                    nUsuarioID = table.Column<int>(type: "int(32)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NEstadoVerificacionFK = table.Column<int>(type: "int(12)", nullable: false),
                    NEstadoUsuarioFK = table.Column<int>(type: "int(12)", nullable: false),
                    NRolFK = table.Column<int>(type: "int(12)", nullable: false),
                    CNombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CApellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CEmail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPassword = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DFechaRegistro = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.nUsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_EstadoUsuarios",
                        column: x => x.NEstadoUsuarioFK,
                        principalTable: "TEstadoUsuario",
                        principalColumn: "NEstadoUsuarioID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_EstadoVerificacion",
                        column: x => x.NEstadoVerificacionFK,
                        principalTable: "TEstadoVerificacion",
                        principalColumn: "NEstadoVerificacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuarios_Rol",
                        column: x => x.NRolFK,
                        principalTable: "TRol",
                        principalColumn: "NRolID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TLogins",
                columns: table => new
                {
                    NLoginID = table.Column<int>(type: "int(32)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DFechaLogin = table.Column<DateTime>(type: "DateTime", nullable: false),
                    NUsuarioFK = table.Column<int>(type: "int(32)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NLoginID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Logins",
                        column: x => x.NUsuarioFK,
                        principalTable: "tUsuarios",
                        principalColumn: "nUsuarioID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TEstadoUsuario",
                columns: new[] { "NEstadoUsuarioID", "CNombre" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Inactivo" }
                });

            migrationBuilder.InsertData(
                table: "TEstadoVerificacion",
                columns: new[] { "NEstadoVerificacion", "CNombre" },
                values: new object[,]
                {
                    { 1, "Verificado" },
                    { 2, "No Verificado" }
                });

            migrationBuilder.InsertData(
                table: "TRol",
                columns: new[] { "NRolID", "CNombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Paciente" },
                    { 3, "Medico" }
                });

            migrationBuilder.CreateIndex(
                name: "CNombre",
                table: "TEstadoUsuario",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre1",
                table: "TEstadoVerificacion",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TLogins_NUsuarioFK",
                table: "TLogins",
                column: "NUsuarioFK");

            migrationBuilder.CreateIndex(
                name: "CNombre2",
                table: "TRol",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre3",
                table: "TTipoIdentificacion",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CEmail",
                table: "tUsuarios",
                column: "CEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tUsuarios_NEstadoUsuarioFK",
                table: "tUsuarios",
                column: "NEstadoUsuarioFK");

            migrationBuilder.CreateIndex(
                name: "IX_tUsuarios_NEstadoVerificacionFK",
                table: "tUsuarios",
                column: "NEstadoVerificacionFK");

            migrationBuilder.CreateIndex(
                name: "IX_tUsuarios_NRolFK",
                table: "tUsuarios",
                column: "NRolFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TLogins");

            migrationBuilder.DropTable(
                name: "TTipoIdentificacion");

            migrationBuilder.DropTable(
                name: "tUsuarios");

            migrationBuilder.DropTable(
                name: "TEstadoUsuario");

            migrationBuilder.DropTable(
                name: "TEstadoVerificacion");

            migrationBuilder.DropTable(
                name: "TRol");
        }
    }
}
