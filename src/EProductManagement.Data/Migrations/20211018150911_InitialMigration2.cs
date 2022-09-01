using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EProductManagement.Data.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    EProductId = table.Column<int>(nullable: false),
                    SenderPartyId = table.Column<int>(nullable: false),
                    ReceiverPartyId = table.Column<int>(nullable: false),
                    Amount = table.Column<long>(nullable: false),
                    UnitValue = table.Column<int>(nullable: false),
                    TotalSalesPrice = table.Column<long>(nullable: false),
                    SenderCommissionAmount = table.Column<long>(nullable: false),
                    ReceiverCommissionAmount = table.Column<long>(nullable: false),
                    SenderInvoiceNo = table.Column<string>(maxLength: 20, nullable: true),
                    ReceiverInvoiceNo = table.Column<string>(maxLength: 20, nullable: true),
                    IsTransferred = table.Column<bool>(nullable: false),
                    RetailPrice = table.Column<long>(nullable: false),
                    StockTransactionTypeId = table.Column<int>(nullable: false),
                    StockTransactionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransaction", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransaction");
        }
    }
}
