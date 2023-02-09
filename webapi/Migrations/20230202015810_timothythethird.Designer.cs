﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Infrastructure.Database.Contexts;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230202015810_timothythethird")]
    partial class timothythethird
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.Admin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.CartItem", b =>
                {
                    b.Property<Guid>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CartItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderPrimaryId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartItemId");

                    b.HasIndex("OrderPrimaryId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.Checkout", b =>
                {
                    b.Property<Guid>("CheckoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderPrimaryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("CheckoutId");

                    b.HasIndex("OrderPrimaryId");

                    b.ToTable("Checkouts");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.Property<Guid>("UserPrimaryID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("UserPrimaryID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.CartItem", b =>
                {
                    b.HasOne("webapi.Infrastructure.Database.Entities.Order", "Order")
                        .WithMany("CartItems")
                        .HasForeignKey("OrderPrimaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.Checkout", b =>
                {
                    b.HasOne("webapi.Infrastructure.Database.Entities.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderPrimaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.Order", b =>
                {
                    b.HasOne("webapi.Infrastructure.Database.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserPrimaryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.Order", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("webapi.Infrastructure.Database.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
