﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RetoApptelinkApi.Data;

#nullable disable

namespace RetoApptelinkApi.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240308185140_newCustomer")]
    partial class newCustomer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.1.24081.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RetoApptelinkApi.Models.Customer", b =>
                {
                    b.Property<int>("Id_Customer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Customer"));

                    b.Property<string>("Address_Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Alias_Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date_Created_Customer")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email_Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Is_Active_Customer")
                        .HasColumnType("int");

                    b.Property<string>("Name_Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RucDni_Customer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Customer");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("RetoApptelinkApi.Models.Invoice", b =>
                {
                    b.Property<int>("Id_Invoice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Invoice"));

                    b.Property<int?>("CustomersId_Customer")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_Created_Invoice")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id_Customer_Invoice")
                        .HasColumnType("int");

                    b.Property<double>("Total_Invoice")
                        .HasColumnType("float");

                    b.HasKey("Id_Invoice");

                    b.HasIndex("CustomersId_Customer");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("RetoApptelinkApi.Models.InvoiceProduct", b =>
                {
                    b.Property<int>("Id_InvoiceProduct")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_InvoiceProduct"));

                    b.Property<int>("Id_Invoice_InvoiceProduct")
                        .HasColumnType("int");

                    b.Property<int>("Id_Product_InvoiceProduct")
                        .HasColumnType("int");

                    b.Property<int?>("InvoiceId_Invoice")
                        .HasColumnType("int");

                    b.Property<double>("Price_InvoiceProduct")
                        .HasColumnType("float");

                    b.Property<int?>("ProductId_Product")
                        .HasColumnType("int");

                    b.Property<int>("Quantity_InvoiceProduct")
                        .HasColumnType("int");

                    b.Property<double>("Total_InvoiceProduct")
                        .HasColumnType("float");

                    b.HasKey("Id_InvoiceProduct");

                    b.HasIndex("InvoiceId_Invoice");

                    b.HasIndex("ProductId_Product");

                    b.ToTable("InvoicesProducts");
                });

            modelBuilder.Entity("RetoApptelinkApi.Models.Product", b =>
                {
                    b.Property<int>("Id_Product")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Product"));

                    b.Property<int>("Code_Product")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_Created_Product")
                        .HasColumnType("datetime2");

                    b.Property<int>("Is_Active_Product")
                        .HasColumnType("int");

                    b.Property<string>("Name_Product")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price_Product")
                        .HasColumnType("float");

                    b.Property<int>("Stock_Product")
                        .HasColumnType("int");

                    b.HasKey("Id_Product");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RetoApptelinkApi.Models.User", b =>
                {
                    b.Property<int>("Id_User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_User"));

                    b.Property<DateTime>("Date_Created_User")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email_User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Is_Active_User")
                        .HasColumnType("int");

                    b.Property<int>("Login_Attempts_User")
                        .HasColumnType("int");

                    b.Property<string>("Name_User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password_User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_User");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RetoApptelinkApi.Models.Invoice", b =>
                {
                    b.HasOne("RetoApptelinkApi.Models.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("CustomersId_Customer");

                    b.Navigation("Customers");
                });

            modelBuilder.Entity("RetoApptelinkApi.Models.InvoiceProduct", b =>
                {
                    b.HasOne("RetoApptelinkApi.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId_Invoice");

                    b.HasOne("RetoApptelinkApi.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId_Product");

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
