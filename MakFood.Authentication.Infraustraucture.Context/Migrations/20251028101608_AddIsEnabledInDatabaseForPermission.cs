using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakFood.Authentication.Infraustraucture.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddIsEnabledInDatabaseForPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Permissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Permissions");
        }
    }
}
