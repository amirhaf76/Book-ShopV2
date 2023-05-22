using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations
{
    /// <inheritdoc />
    public partial class RoleIdaddedtoUserPermissiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserPermission",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPermission_RoleId",
                table: "UserPermission",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Role_RoleId",
                table: "UserPermission",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermission_Role_RoleId",
                table: "UserPermission");

            migrationBuilder.DropIndex(
                name: "IX_UserPermission_RoleId",
                table: "UserPermission");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserPermission");
        }
    }
}
