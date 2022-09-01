using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EProductManagement.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    PhotoUrl = table.Column<string>(maxLength: 300, nullable: true),
                    FirstStockLevel = table.Column<int>(nullable: false),
                    CurrentStockLevel = table.Column<int>(nullable: false),
                    MaxPax = table.Column<int>(nullable: false),
                    LastUseDate = table.Column<DateTime>(nullable: true),
                    RetailPrice = table.Column<long>(nullable: false),
                    SalesPrice = table.Column<long>(nullable: false),
                    MerchantId = table.Column<int>(nullable: false),
                    IsTransferrable = table.Column<bool>(nullable: false),
                    IsStockout = table.Column<bool>(nullable: false),
                    SettlementDate = table.Column<DateTime>(nullable: false),
                    IsConvertibleToEMoney = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EProduct_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EProduct_CategoryId",
                table: "EProduct",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EProduct");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
