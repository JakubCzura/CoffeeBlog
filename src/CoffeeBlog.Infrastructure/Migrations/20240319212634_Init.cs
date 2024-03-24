using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeBlog.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ApiError",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Exception = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApiError", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserDetail",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastSuccessfullSignIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastFailedSignIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastUsernameChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastEmailChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastPasswordChange = table.Column<DateTime>(type: "datetime2", nullable: true),
                UserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserDetail", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "RequestDetail",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ControllerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                HttpMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                StatusCode = table.Column<int>(type: "int", nullable: false),
                RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                RequestContentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ResponseContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                RequestTimeInMiliseconds = table.Column<long>(type: "bigint", nullable: false),
                SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RequestDetail", x => x.Id);
                table.ForeignKey(
                    name: "FK_RequestDetail_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "UserLastPassword",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                LastPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UserId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserLastPassword", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserLastPassword_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserToRole",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                RoleId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserToRole", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserToRole_Role_RoleId",
                    column: x => x.RoleId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserToRole_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_RequestDetail_UserId",
            table: "RequestDetail",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_User_Username_Email",
            table: "User",
            columns: new[] { "Username", "Email" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserLastPassword_UserId",
            table: "UserLastPassword",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserToRole_RoleId",
            table: "UserToRole",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "IX_UserToRole_UserId",
            table: "UserToRole",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ApiError");

        migrationBuilder.DropTable(
            name: "RequestDetail");

        migrationBuilder.DropTable(
            name: "UserDetail");

        migrationBuilder.DropTable(
            name: "UserLastPassword");

        migrationBuilder.DropTable(
            name: "UserToRole");

        migrationBuilder.DropTable(
            name: "Role");

        migrationBuilder.DropTable(
            name: "User");
    }
}
