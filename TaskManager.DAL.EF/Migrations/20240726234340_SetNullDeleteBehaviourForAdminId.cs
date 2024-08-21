using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class SetNullDeleteBehaviourForAdminId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_AdminId",
                table: "projects");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 27, 2, 43, 38, 17, DateTimeKind.Local).AddTicks(3304));

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_AdminId",
                table: "projects",
                column: "AdminId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_users_AdminId",
                table: "projects");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 27, 2, 8, 10, 102, DateTimeKind.Local).AddTicks(4547));

            migrationBuilder.AddForeignKey(
                name: "FK_projects_users_AdminId",
                table: "projects",
                column: "AdminId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
