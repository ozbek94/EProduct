using Microsoft.EntityFrameworkCore.Migrations;

namespace EProductManagement.Data.Migrations
{
    public partial class InitialMigration8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "UpperCategoryId",
                table: "Category",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpperCategoryId",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Category",
                type: "integer",
                nullable: true);
        }
    }
}
