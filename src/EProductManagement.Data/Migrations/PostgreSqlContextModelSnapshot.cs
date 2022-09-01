﻿// <auto-generated />
using System;
using EProductManagement.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EProductManagement.Data.Migrations
{
    [DbContext(typeof(PostgreSqlContext))]
    partial class PostgreSqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("UpperCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.EProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("CurrentStockLevel")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FirstStockLevel")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

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

            modelBuilder.Entity("EProductManagement.Domain.Entities.ProductBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EProductId")
                        .HasColumnType("integer");

                    b.Property<int>("In")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("MonetaryTransactionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Out")
                        .HasColumnType("integer");

                    b.Property<int>("PartyId")
                        .HasColumnType("integer");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EProductId");

                    b.ToTable("ProductBalance");
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.Redemption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EproductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PartyId")
                        .HasColumnType("integer");

                    b.Property<string>("QrDate")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("StockTransactionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EproductId");

                    b.HasIndex("StockTransactionId");

                    b.ToTable("Redemption");
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.StockTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("Amount")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("EProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsTransferred")
                        .HasColumnType("boolean");

                    b.Property<long>("ReceiverCommissionAmount")
                        .HasColumnType("bigint");

                    b.Property<string>("ReceiverInvoiceNo")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<int>("ReceiverPartyId")
                        .HasColumnType("integer");

                    b.Property<long>("RetailPrice")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderCommissionAmount")
                        .HasColumnType("bigint");

                    b.Property<string>("SenderInvoiceNo")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20);

                    b.Property<int>("SenderPartyId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int>("StockTransactionTypeId")
                        .HasColumnType("integer");

                    b.Property<long>("TotalSalesPrice")
                        .HasColumnType("bigint");

                    b.Property<int>("UnitValue")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("StockTransaction");
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.EProduct", b =>
                {
                    b.HasOne("EProductManagement.Domain.Entities.Category", "Category")
                        .WithMany("EProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.ProductBalance", b =>
                {
                    b.HasOne("EProductManagement.Domain.Entities.EProduct", "EProduct")
                        .WithMany()
                        .HasForeignKey("EProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EProductManagement.Domain.Entities.Redemption", b =>
                {
                    b.HasOne("EProductManagement.Domain.Entities.EProduct", "EProduct")
                        .WithMany()
                        .HasForeignKey("EproductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EProductManagement.Domain.Entities.StockTransaction", "StockTransaction")
                        .WithMany()
                        .HasForeignKey("StockTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
