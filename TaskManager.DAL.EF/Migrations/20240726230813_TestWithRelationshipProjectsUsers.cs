using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Api.Migrations
{
    /// <inheritdoc />
    public partial class TestWithRelationshipProjectsUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users_projects");

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    ProjectUsersId = table.Column<int>(type: "int", nullable: false),
                    UserProjectsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.ProjectUsersId, x.UserProjectsId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_projects_UserProjectsId",
                        column: x => x.UserProjectsId,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProjectUser_users_ProjectUsersId",
                        column: x => x.ProjectUsersId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 7, 27, 2, 8, 10, 102, DateTimeKind.Local).AddTicks(4547));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_UserProjectsId",
                table: "ProjectUser",
                column: "UserProjectsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.CreateTable(
                name: "users_projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    project_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_projects_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_users_projects_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "registr_date",
                value: new DateTime(2024, 6, 17, 10, 48, 34, 990, DateTimeKind.Local).AddTicks(6370));

            migrationBuilder.CreateIndex(
                name: "IX_users_projects_project_id",
                table: "users_projects",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_projects_user_id",
                table: "users_projects",
                column: "user_id");
        }
    }
}
