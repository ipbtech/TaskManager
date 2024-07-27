using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class TestWithProjectRels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_projects_UserProjectsId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_users_ProjectUsersId",
                table: "ProjectUser");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 27, 3, 14, 6, 929, DateTimeKind.Local).AddTicks(6210));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_projects_UserProjectsId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_users_ProjectUsersId",
                table: "ProjectUser");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 27, 2, 43, 38, 17, DateTimeKind.Local).AddTicks(3304));

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
    }
}
