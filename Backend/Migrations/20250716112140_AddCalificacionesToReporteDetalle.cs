using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCalificacionesToReporteDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportesDetalles_Calificaciones_CalificacionId",
                table: "ReportesDetalles");

            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropIndex(
                name: "IX_ReportesDetalles_CalificacionId",
                table: "ReportesDetalles");

            migrationBuilder.DropColumn(
                name: "CalificacionId",
                table: "ReportesDetalles");

            migrationBuilder.UpdateData(
                table: "ReportesDetalles",
                keyColumn: "ActividadDescripcion",
                keyValue: null,
                column: "ActividadDescripcion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ActividadDescripcion",
                table: "ReportesDetalles",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Atencion",
                table: "ReportesDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Documentos",
                table: "ReportesDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Elementos",
                table: "ReportesDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Horario",
                table: "ReportesDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Presencia",
                table: "ReportesDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Puestos",
                table: "ReportesDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Atencion",
                table: "ReportesDetalles");

            migrationBuilder.DropColumn(
                name: "Documentos",
                table: "ReportesDetalles");

            migrationBuilder.DropColumn(
                name: "Elementos",
                table: "ReportesDetalles");

            migrationBuilder.DropColumn(
                name: "Horario",
                table: "ReportesDetalles");

            migrationBuilder.DropColumn(
                name: "Presencia",
                table: "ReportesDetalles");

            migrationBuilder.DropColumn(
                name: "Puestos",
                table: "ReportesDetalles");

            migrationBuilder.AlterColumn<string>(
                name: "ActividadDescripcion",
                table: "ReportesDetalles",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CalificacionId",
                table: "ReportesDetalles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Atencion = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Documentos = table.Column<int>(type: "int", nullable: false),
                    Elementos = table.Column<int>(type: "int", nullable: false),
                    Horario = table.Column<int>(type: "int", nullable: false),
                    Presencia = table.Column<int>(type: "int", nullable: false),
                    Puestos = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    VigiladorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReportesDetalles_CalificacionId",
                table: "ReportesDetalles",
                column: "CalificacionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportesDetalles_Calificaciones_CalificacionId",
                table: "ReportesDetalles",
                column: "CalificacionId",
                principalTable: "Calificaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
