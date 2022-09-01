using Microsoft.EntityFrameworkCore.Migrations;

namespace EProductManagement.Data.Migrations
{
    public partial class InitialMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockTransactionType",
                table: "StockTransaction");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalance_EProductId",
                table: "ProductBalance",
                column: "EProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBalance_EProduct_EProductId",
                table: "ProductBalance",
                column: "EProductId",
                principalTable: "EProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBalance_EProduct_EProductId",
                table: "ProductBalance");

            migrationBuilder.DropIndex(
                name: "IX_ProductBalance_EProductId",
                table: "ProductBalance");

            migrationBuilder.AddColumn<int>(
                name: "StockTransactionType",
                table: "StockTransaction",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
