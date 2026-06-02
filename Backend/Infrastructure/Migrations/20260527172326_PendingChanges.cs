using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_status",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_status",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_priority",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_priority",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_notification",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_notification",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_history",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_history",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_category",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_category",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_attachment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_attachment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "company",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "company",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "comment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "comment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(UTC_TIMESTAMP())");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "user",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_status",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_status",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_priority",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_priority",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_notification",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_notification",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_history",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_history",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_category",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_category",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket_attachment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket_attachment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ticket",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ticket",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "company",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "company",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "comment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "comment",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "(UTC_TIMESTAMP())",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "(SYSUTCDATETIME())");
        }
    }
}
