﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toro.Repository.Context;

namespace Toro.Repository.Migrations
{
    [DbContext(typeof(ToroContext))]
    [Migration("20220411053248_versao2")]
    partial class versao2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("Toro.Domain.Entity.Asset", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("Symbol");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("TEXT")
                        .HasColumnName("CurrentPrice");

                    b.Property<DateTime>("RegisteringDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("RegisteringDate");

                    b.HasKey("Id");

                    b.ToTable("Asset");
                });

            modelBuilder.Entity("Toro.Domain.Entity.AssetXPatrimony", b =>
                {
                    b.Property<int>("PatrimonyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AssetId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("RegisteringDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("RegisteringDate");

                    b.HasKey("PatrimonyId", "AssetId", "Id");

                    b.HasIndex("AssetId");

                    b.ToTable("AssetXPatrimony");
                });

            modelBuilder.Entity("Toro.Domain.Entity.Investor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Account")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Branch")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Cpf");

                    b.Property<DateTime>("RegisteringDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("RegisteringDate");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Investor");
                });

            modelBuilder.Entity("Toro.Domain.Entity.Patrimony", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("AccountAmount")
                        .HasColumnType("TEXT")
                        .HasColumnName("AccountAmount");

                    b.Property<int>("InvestorId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("InvestorId");

                    b.Property<DateTime>("RegisteringDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("RegisteringDate");

                    b.HasKey("Id");

                    b.HasIndex("InvestorId");

                    b.ToTable("Patrimony");
                });

            modelBuilder.Entity("Toro.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("InvestorId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("InvestorId");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Password");

                    b.Property<DateTime>("RegisteringDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("RegisteringDate");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Toro.Domain.Entity.AssetXPatrimony", b =>
                {
                    b.HasOne("Toro.Domain.Entity.Asset", "Asset")
                        .WithMany("AssetXPatrimony")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Toro.Domain.Entity.Patrimony", "Patrimony")
                        .WithMany("AssetXPatrimony")
                        .HasForeignKey("PatrimonyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Patrimony");
                });

            modelBuilder.Entity("Toro.Domain.Entity.Investor", b =>
                {
                    b.HasOne("Toro.Domain.Entity.User", "User")
                        .WithOne()
                        .HasForeignKey("Toro.Domain.Entity.Investor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Toro.Domain.Entity.Patrimony", b =>
                {
                    b.HasOne("Toro.Domain.Entity.Investor", "Investor")
                        .WithMany()
                        .HasForeignKey("InvestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Investor");
                });

            modelBuilder.Entity("Toro.Domain.Entity.Asset", b =>
                {
                    b.Navigation("AssetXPatrimony");
                });

            modelBuilder.Entity("Toro.Domain.Entity.Patrimony", b =>
                {
                    b.Navigation("AssetXPatrimony");
                });
#pragma warning restore 612, 618
        }
    }
}