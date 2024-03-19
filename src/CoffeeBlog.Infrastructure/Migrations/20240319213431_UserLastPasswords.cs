using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserLastPasswords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLastPassword_User_UserId",
                table: "UserLastPassword");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLastPassword",
                table: "UserLastPassword");

            migrationBuilder.RenameTable(
                name: "UserLastPassword",
                newName: "UserLastPasswords");

            migrationBuilder.RenameIndex(
                name: "IX_UserLastPassword_UserId",
                table: "UserLastPasswords",
                newName: "IX_UserLastPasswords_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLastPasswords",
                table: "UserLastPasswords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLastPasswords_User_UserId",
                table: "UserLastPasswords",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLastPasswords_User_UserId",
                table: "UserLastPasswords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLastPasswords",
                table: "UserLastPasswords");

            migrationBuilder.RenameTable(
                name: "UserLastPasswords",
                newName: "UserLastPassword");

            migrationBuilder.RenameIndex(
                name: "IX_UserLastPasswords_UserId",
                table: "UserLastPassword",
                newName: "IX_UserLastPassword_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLastPassword",
                table: "UserLastPassword",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLastPassword_User_UserId",
                table: "UserLastPassword",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
