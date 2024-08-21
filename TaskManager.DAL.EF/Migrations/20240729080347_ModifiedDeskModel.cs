using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedDeskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_desks_users_DeskOwnerId",
                table: "desks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_projects_UserProjectsId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_users_ProjectUsersId",
                table: "ProjectUser");

            migrationBuilder.DropIndex(
                name: "IX_desks_DeskOwnerId",
                table: "desks");

            migrationBuilder.DropColumn(
                name: "DeskOwnerId",
                table: "desks");

            migrationBuilder.DropColumn(
                name: "private",
                table: "desks");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 29, 11, 3, 46, 171, DateTimeKind.Local).AddTicks(2875));

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_projects_UserProjectsId",
                table: "ProjectUser",
                column: "UserProjectsId",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_users_ProjectUsersId",
                table: "ProjectUser",
                column: "ProjectUsersId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_projects_UserProjectsId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_users_ProjectUsersId",
                table: "ProjectUser");

            migrationBuilder.AddColumn<int>(
                name: "DeskOwnerId",
                table: "desks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "private",
                table: "desks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 27, 3, 14, 6, 929, DateTimeKind.Local).AddTicks(6210));

            migrationBuilder.CreateIndex(
                name: "IX_desks_DeskOwnerId",
                table: "desks",
                column: "DeskOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_desks_users_DeskOwnerId",
                table: "desks",
                column: "DeskOwnerId",
                principalTable: "users",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_projects_UserProjectsId",
                table: "ProjectUser",
                column: "UserProjectsId",
                principalTable: "projects",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_users_ProjectUsersId",
                table: "ProjectUser",
                column: "ProjectUsersId",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
