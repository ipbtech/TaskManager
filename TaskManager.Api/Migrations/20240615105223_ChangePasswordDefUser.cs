using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangePasswordDefUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Password", "registr_date" },
                values: new object[] { "65e84be33532fb784c48129675f9eff3a682b27168c0ea744b2cf58ee02337c5", new DateTime(2024, 6, 15, 13, 52, 23, 208, DateTimeKind.Local).AddTicks(1450) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Password", "registr_date" },
                values: new object[] { "qwerty", new DateTime(2024, 6, 15, 13, 43, 56, 647, DateTimeKind.Local).AddTicks(9773) });
        }
    }
}
