// <auto-generated />
using System;
using EProductManagement.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EProductManagement.Data.Migrations
{
    [DbContext(typeof(PostgreSqlContext))]
    [Migration("20211015142814_Initial Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EProductManagement.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.EProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("CurrentStockLevel")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FirstStockLevel")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsConvertibleToEMoney")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsStockout")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsTransferrable")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastUseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MaxPax")
                        .HasColumnType("integer");

                    b.Property<int>("MerchantId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<long>("RetailPrice")
                        .HasColumnType("bigint");

                    b.Property<long>("SalesPrice")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("EProduct");
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.EProduct", b =>
                {
                    b.HasOne("EProductManagement.Domain.Entities.Category", "Category")
                        .WithMany("EProducts")
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
