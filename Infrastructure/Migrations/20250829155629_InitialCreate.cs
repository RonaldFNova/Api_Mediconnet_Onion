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
                name: "TDiaSemana",
                columns: table => new
                {
                    NDiaSemanaID = table.Column<int>(type: "int(12)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNombre = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NDiaSemanaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TEspecialidad",
                columns: table => new
                {
                    NEspecialidadID = table.Column<int>(type: "int(12)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNombre = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CDescripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NEspecialidadID);
                })
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
                name: "TGrupoSanguineo",
                columns: table => new
                {
                    NGrupoSanguineoID = table.Column<int>(type: "int(12)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CNombre = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NGrupoSanguineoID);
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
                name: "TUsuarios",
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
                        principalTable: "TUsuarios",
                        principalColumn: "nUsuarioID",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TPersona",
                columns: table => new
                {
                    NPersonaID = table.Column<int>(type: "int(32)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NUsuarioFK = table.Column<int>(type: "int(32)", nullable: false),
                    NTipoIdentificacionFK = table.Column<int>(type: "int(32)", nullable: false),
                    CNroIdentificacion = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CNroConctacto = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CDireccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DFechaNacimiento = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ESexo = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NPersonaID);
                    table.ForeignKey(
                        name: "FK_Personas_TipoIdentificacion",
                        column: x => x.NTipoIdentificacionFK,
                        principalTable: "TTipoIdentificacion",
                        principalColumn: "NTipoIdentificacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TPersona_TUsuarios_NUsuarioFK",
                        column: x => x.NUsuarioFK,
                        principalTable: "TUsuarios",
                        principalColumn: "nUsuarioID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TPaciente",
                columns: table => new
                {
                    NPacienteID = table.Column<int>(type: "int(32)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NPersonaFK = table.Column<int>(type: "int(32)", nullable: false),
                    NGrupoSanguineoFK = table.Column<int>(type: "int(6)", nullable: false),
                    CAlergiasGenerales = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NPacienteID);
                    table.ForeignKey(
                        name: "FK_TPaciente_TPersona_NPersonaFK",
                        column: x => x.NPersonaFK,
                        principalTable: "TPersona",
                        principalColumn: "NPersonaID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TProfesional",
                columns: table => new
                {
                    NProfesionalID = table.Column<int>(type: "int(32)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NPersonaFK = table.Column<int>(type: "int(32)", nullable: false),
                    CRegistroProfesional = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DFechaContratacion = table.Column<DateTime>(type: "DateTime", nullable: false),
                    ETipoProfesional = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CBiografia = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.NProfesionalID);
                    table.ForeignKey(
                        name: "FK_TProfesional_TPersona_NPersonaFK",
                        column: x => x.NPersonaFK,
                        principalTable: "TPersona",
                        principalColumn: "NPersonaID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TDiaSemana",
                columns: new[] { "NDiaSemanaID", "CNombre" },
                values: new object[,]
                {
                    { 1, "Lunes" },
                    { 2, "Martes" },
                    { 3, "Miércoles" },
                    { 4, "Jueves" },
                    { 5, "Viernes" },
                    { 6, "Sábado" },
                    { 7, "Domingo" }
                });

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
                table: "TGrupoSanguineo",
                columns: new[] { "NGrupoSanguineoID", "CNombre" },
                values: new object[,]
                {
                    { 1, "A+" },
                    { 2, "A-" },
                    { 3, "B+" },
                    { 4, "B-" },
                    { 5, "AB+" },
                    { 6, "AB-" },
                    { 7, "O+" },
                    { 8, "O-" }
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

            migrationBuilder.InsertData(
                table: "TTipoIdentificacion",
                columns: new[] { "NTipoIdentificacion", "CNombre" },
                values: new object[,]
                {
                    { 1, "Cédula de ciudadanía" },
                    { 2, "Tarjeta de identidad" },
                    { 3, "Registro civil de nacimiento" },
                    { 4, "Cédula de extranjería" },
                    { 5, "Pasaporte" }
                });

            migrationBuilder.CreateIndex(
                name: "CNombre",
                table: "TDiaSemana",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre1",
                table: "TEspecialidad",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre2",
                table: "TEstadoUsuario",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre3",
                table: "TEstadoVerificacion",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre4",
                table: "TGrupoSanguineo",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TLogins_NUsuarioFK",
                table: "TLogins",
                column: "NUsuarioFK");

            migrationBuilder.CreateIndex(
                name: "IX_TPaciente_NPersonaFK",
                table: "TPaciente",
                column: "NPersonaFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNroIdentificacion",
                table: "TPersona",
                column: "CNroIdentificacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TPersona_NTipoIdentificacionFK",
                table: "TPersona",
                column: "NTipoIdentificacionFK");

            migrationBuilder.CreateIndex(
                name: "IX_TPersona_NUsuarioFK",
                table: "TPersona",
                column: "NUsuarioFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CRegistroProfesional",
                table: "TProfesional",
                column: "CRegistroProfesional",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TProfesional_NPersonaFK",
                table: "TProfesional",
                column: "NPersonaFK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre5",
                table: "TRol",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CNombre6",
                table: "TTipoIdentificacion",
                column: "CNombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CEmail",
                table: "TUsuarios",
                column: "CEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TUsuarios_NEstadoUsuarioFK",
                table: "TUsuarios",
                column: "NEstadoUsuarioFK");

            migrationBuilder.CreateIndex(
                name: "IX_TUsuarios_NEstadoVerificacionFK",
                table: "TUsuarios",
                column: "NEstadoVerificacionFK");

            migrationBuilder.CreateIndex(
                name: "IX_TUsuarios_NRolFK",
                table: "TUsuarios",
                column: "NRolFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TDiaSemana");

            migrationBuilder.DropTable(
                name: "TEspecialidad");

            migrationBuilder.DropTable(
                name: "TGrupoSanguineo");

            migrationBuilder.DropTable(
                name: "TLogins");

            migrationBuilder.DropTable(
                name: "TPaciente");

            migrationBuilder.DropTable(
                name: "TProfesional");

            migrationBuilder.DropTable(
                name: "TPersona");

            migrationBuilder.DropTable(
                name: "TTipoIdentificacion");

            migrationBuilder.DropTable(
                name: "TUsuarios");

            migrationBuilder.DropTable(
                name: "TEstadoUsuario");

            migrationBuilder.DropTable(
                name: "TEstadoVerificacion");

            migrationBuilder.DropTable(
                name: "TRol");
        }
    }
}
