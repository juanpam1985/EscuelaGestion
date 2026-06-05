using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscuelaGestion.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
                    estudiante_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    grado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiantes", x => x.estudiante_id);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    profesor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    especialidad = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.profesor_id);
                });

            migrationBuilder.CreateTable(
                name: "Clases",
                columns: table => new
                {
                    clase_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    horario = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    profesor_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clases", x => x.clase_id);
                    table.ForeignKey(
                        name: "FK_Clases_Profesores_profesor_id",
                        column: x => x.profesor_id,
                        principalTable: "Profesores",
                        principalColumn: "profesor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AsignacionesClases",
                columns: table => new
                {
                    asignacion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estudiante_id = table.Column<int>(type: "int", nullable: false),
                    clase_id = table.Column<int>(type: "int", nullable: false),
                    fecha_asignacion = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsignacionesClases", x => x.asignacion_id);
                    table.ForeignKey(
                        name: "FK_AsignacionesClases_Clases_clase_id",
                        column: x => x.clase_id,
                        principalTable: "Clases",
                        principalColumn: "clase_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AsignacionesClases_Estudiantes_estudiante_id",
                        column: x => x.estudiante_id,
                        principalTable: "Estudiantes",
                        principalColumn: "estudiante_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesClases_clase_id",
                table: "AsignacionesClases",
                column: "clase_id");

            migrationBuilder.CreateIndex(
                name: "IX_AsignacionesClases_estudiante_id",
                table: "AsignacionesClases",
                column: "estudiante_id");

            migrationBuilder.CreateIndex(
                name: "IX_Clases_profesor_id",
                table: "Clases",
                column: "profesor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AsignacionesClases");

            migrationBuilder.DropTable(
                name: "Clases");

            migrationBuilder.DropTable(
                name: "Estudiantes");

            migrationBuilder.DropTable(
                name: "Profesores");
        }
    }
}
