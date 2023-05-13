using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.Migrations
{
    /// <inheritdoc />
    public partial class RenamingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "ZipCodes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Addresses",
                newName: "AddressLine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "ZipCodes",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "AddressLine",
                table: "Addresses",
                newName: "Address");
        }
    }
}
