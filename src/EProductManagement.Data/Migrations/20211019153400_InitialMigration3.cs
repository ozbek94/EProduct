using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EProductManagement.Data.Migrations
{
    public partial class InitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "StockTransaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductBalance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    EProductId = table.Column<int>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    In = table.Column<int>(nullable: false),
                    Out = table.Column<int>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    MonetaryTransaction = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Redemption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsertTime = table.Column<DateTime>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    PartyId = table.Column<int>(nullable: false),
                    StockTransactionId = table.Column<int>(nullable: false),
                    EproductId = table.Column<int>(nullable: false),
                    QrDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redemption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Redemption_EProduct_EproductId",
                        column: x => x.EproductId,
                        principalTable: "EProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Redemption_StockTransaction_StockTransactionId",
                        column: x => x.StockTransactionId,
                        principalTable: "StockTransaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Redemption_EproductId",
                table: "Redemption",
                column: "EproductId");

            migrationBuilder.CreateIndex(
                name: "IX_Redemption_StockTransactionId",
                table: "Redemption",
                column: "StockTransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductBalance");

            migrationBuilder.DropTable(
                name: "Redemption");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "StockTransaction");
        }
    }
}
