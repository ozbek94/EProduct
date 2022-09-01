using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EProductManagement.Data.Migrations
{
    public partial class InitialMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonetaryTransaction",
                table: "ProductBalance");

            migrationBuilder.AddColumn<Guid>(
                name: "MonetaryTransactionId",
                table: "ProductBalance",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonetaryTransactionId",
                table: "ProductBalance");

            migrationBuilder.AddColumn<bool>(
                name: "MonetaryTransaction",
                table: "ProductBalance",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
