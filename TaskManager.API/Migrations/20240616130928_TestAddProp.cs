using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class TestAddProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HashPassword",
                table: "users",
                newName: "hash_password");

            migrationBuilder.AddColumn<string>(
                name: "TestProp",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "registr_date", "TestProp" },
                values: new object[] { new DateTime(2024, 6, 16, 16, 9, 28, 350, DateTimeKind.Local).AddTicks(7202), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestProp",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "hash_password",
                table: "users",
                newName: "HashPassword");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 6, 16, 16, 7, 21, 806, DateTimeKind.Local).AddTicks(9047));
        }
    }
}
