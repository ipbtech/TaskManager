using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "attachments",
                table: "tasks");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 8, 21, 15, 55, 1, 950, DateTimeKind.Local).AddTicks(4058));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "attachments",
                table: "tasks",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 29, 11, 3, 46, 171, DateTimeKind.Local).AddTicks(2875));
        }
    }
}
