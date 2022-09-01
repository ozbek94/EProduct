using Microsoft.EntityFrameworkCore.Migrations;

namespace EProductManagement.Data.Migrations
{
    public partial class InitialMigration10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EProduct_Category_CategoryId",
                table: "EProduct");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "EProduct",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EProduct_Category_CategoryId",
                table: "EProduct",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EProduct_Category_CategoryId",
                table: "EProduct");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "EProduct",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EProduct_Category_CategoryId",
                table: "EProduct",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
