using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAvatarProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 6, 16, 23, 7, 58, 267, DateTimeKind.Local).AddTicks(6581));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "avatar",
                table: "users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "avatar", "registr_date" },
                values: new object[] { null, new DateTime(2024, 6, 16, 16, 11, 45, 140, DateTimeKind.Local).AddTicks(3433) });
        }
    }
}
