using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakFood.Authentication.Infraustraucture.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddingPermissionStatusToPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Permissions");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
