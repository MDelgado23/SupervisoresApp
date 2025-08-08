using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddEntidadesYRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VigiladorId = table.Column<int>(type: "int", nullable: false),
                    Horario = table.Column<int>(type: "int", nullable: false),
                    Presencia = table.Column<int>(type: "int", nullable: false),
                    Elementos = table.Column<int>(type: "int", nullable: false),
                    Documentos = table.Column<int>(type: "int", nullable: false),
                    Atencion = table.Column<int>(type: "int", nullable: false),
                    Puestos = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Patente = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Marca = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modelo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Km = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vigiladores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vigiladores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VehiculoId = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    KmInicio = table.Column<int>(type: "int", nullable: false),
                    KmFin = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HsTotales = table.Column<double>(type: "double", nullable: false),
                    HsTranslados = table.Column<double>(type: "double", nullable: false),
                    HsSupervision = table.Column<double>(type: "double", nullable: false),
                    HsApoyo = table.Column<double>(type: "double", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reportes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reportes_Vehiculos_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReportesDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReporteId = table.Column<int>(type: "int", nullable: false),
                    ObjetivoId = table.Column<int>(type: "int", nullable: false),
                    VigiladorId = table.Column<int>(type: "int", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActividadId = table.Column<int>(type: "int", nullable: false),
                    ActividadDescripcion = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CalificacionId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportesDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportesDetalles_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportesDetalles_Calificaciones_CalificacionId",
                        column: x => x.CalificacionId,
                        principalTable: "Calificaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ReportesDetalles_Objetivos_ObjetivoId",
                        column: x => x.ObjetivoId,
                        principalTable: "Objetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportesDetalles_Reportes_ReporteId",
                        column: x => x.ReporteId,
                        principalTable: "Reportes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportesDetalles_Vigiladores_VigiladorId",
                        column: x => x.VigiladorId,
                        principalTable: "Vigiladores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_UsuarioId",
                table: "Reportes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_VehiculoId",
                table: "Reportes",
                column: "VehiculoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesDetalles_ActividadId",
                table: "ReportesDetalles",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesDetalles_CalificacionId",
                table: "ReportesDetalles",
                column: "CalificacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesDetalles_ObjetivoId",
                table: "ReportesDetalles",
                column: "ObjetivoId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesDetalles_ReporteId",
                table: "ReportesDetalles",
                column: "ReporteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesDetalles_VigiladorId",
                table: "ReportesDetalles",
                column: "VigiladorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportesDetalles");

            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Vigiladores");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");
        }
    }
}
