﻿// <auto-generated />
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace bangazon.Migrations
{
    [DbContext(typeof(BangazonContext))]
    [Migration("20180126195459_testingMigration")]
    partial class testingMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Bangazon.Models.Computer", b =>
                {
                    b.Property<int>("ComputerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateDecommisioned");

                    b.Property<DateTime>("DatePurchased");

                    b.Property<bool>("Functioning");

                    b.HasKey("ComputerId");

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("Bangazon.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<DateTime>("LastActive");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Bangazon.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Budget");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Bangazon.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartmentId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<int>("Supervisor");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Bangazon.Models.EmployeeComputer", b =>
                {
                    b.Property<int>("EmployeeComputerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComputerId");

                    b.Property<int>("EmployeeId");

                    b.Property<DateTime>("IssueDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<DateTime>("ReturnDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("EmployeeComputerId");

                    b.HasIndex("ComputerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeComputer");
                });

            modelBuilder.Entity("Bangazon.Models.EmployeeTraining", b =>
                {
                    b.Property<int>("EmployeeTrainingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<int>("TrainingProgramId");

                    b.HasKey("EmployeeTrainingId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TrainingProgramId");

                    b.ToTable("EmployeeTraining");
                });

            modelBuilder.Entity("Bangazon.Models.OrderedProduct", b =>
                {
                    b.Property<int>("OrderedProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ProductId");

                    b.Property<int>("ShoppingCartId");

                    b.HasKey("OrderedProductId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("OrderedProduct");
                });

            modelBuilder.Entity("Bangazon.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountNumber");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("PaymentTypeId");

                    b.HasIndex("CustomerId");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("Bangazon.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("DateAdded")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(140);

                    b.Property<int>("Price");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<int>("ProductTypeId");

                    b.Property<int>("Quantity");

                    b.HasKey("ProductId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Bangazon.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("Bangazon.Models.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d %H:%M:%S')");

                    b.Property<DateTime>("DateOrdered");

                    b.Property<int>("PaymentTypeId");

                    b.HasKey("ShoppingCartId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentTypeId");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("Bangazon.Models.TrainingProgram", b =>
                {
                    b.Property<int>("TrainingProgramId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(150);

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("MaxAttendees");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(55);

                    b.Property<DateTime>("StartDate");

                    b.HasKey("TrainingProgramId");

                    b.ToTable("TrainingProgram");
                });

            modelBuilder.Entity("Bangazon.Models.Employee", b =>
                {
                    b.HasOne("Bangazon.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.EmployeeComputer", b =>
                {
                    b.HasOne("Bangazon.Models.Computer", "Computer")
                        .WithMany()
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.EmployeeTraining", b =>
                {
                    b.HasOne("Bangazon.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.TrainingProgram", "TrainingProgram")
                        .WithMany()
                        .HasForeignKey("TrainingProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.OrderedProduct", b =>
                {
                    b.HasOne("Bangazon.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.ShoppingCart", "ShoppingCart")
                        .WithMany()
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.PaymentType", b =>
                {
                    b.HasOne("Bangazon.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.Product", b =>
                {
                    b.HasOne("Bangazon.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bangazon.Models.ShoppingCart", b =>
                {
                    b.HasOne("Bangazon.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Bangazon.Models.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
