using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class CambiarHorasATimeSpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraSalida",
                table: "ReportesDetalles",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraEntrada",
                table: "ReportesDetalles",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HsTranslados",
                table: "Reportes",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HsTotales",
                table: "Reportes",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HsSupervision",
                table: "Reportes",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HsApoyo",
                table: "Reportes",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraInicio",
                table: "Reportes",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "HoraFin",
                table: "Reportes",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraSalida",
                table: "ReportesDetalles",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraEntrada",
                table: "ReportesDetalles",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HsTranslados",
                table: "Reportes",
                type: "double",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HsTotales",
                table: "Reportes",
                type: "double",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HsSupervision",
                table: "Reportes",
                type: "double",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "HsApoyo",
                table: "Reportes",
                type: "double",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraInicio",
                table: "Reportes",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraFin",
                table: "Reportes",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);
        }
    }
}
