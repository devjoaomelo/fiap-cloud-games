﻿// <auto-generated />
using System;
using FCG.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FCG.Infra.Migrations
{
    [DbContext(typeof(FCGDbContext))]
    [Migration("20250801195834_Teste")]
    partial class Teste
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FCG.Domain.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Games", (string)null);
                });

            modelBuilder.Entity("FCG.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("FCG.Domain.Entities.UserGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId", "GameId")
                        .IsUnique();

                    b.ToTable("UserGames", (string)null);
                });

            modelBuilder.Entity("FCG.Domain.Entities.Game", b =>
                {
                    b.OwnsOne("FCG.Domain.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("varchar(1000)")
                                .HasColumnName("Description");

                            b1.HasKey("GameId");

                            b1.ToTable("Games");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsOne("FCG.Domain.ValueObjects.Price", "Price", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("char(36)");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(10,2)")
                                .HasColumnName("Price");

                            b1.HasKey("GameId");

                            b1.ToTable("Games");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsOne("FCG.Domain.ValueObjects.Title", "Title", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("varchar(150)")
                                .HasColumnName("Title");

                            b1.HasKey("GameId");

                            b1.ToTable("Games");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.Navigation("Description")
                        .IsRequired();

                    b.Navigation("Price")
                        .IsRequired();

                    b.Navigation("Title")
                        .IsRequired();
                });

            modelBuilder.Entity("FCG.Domain.Entities.User", b =>
                {
                    b.OwnsOne("FCG.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(254)
                                .HasColumnType("varchar(254)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.HasIndex("Address")
                                .IsUnique();

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("FCG.Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Hash")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("varchar(60)")
                                .HasColumnName("PasswordHash");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("FCG.Domain.Entities.UserGame", b =>
                {
                    b.HasOne("FCG.Domain.Entities.Game", "Game")
                        .WithMany("UserGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FCG.Domain.Entities.User", "User")
                        .WithMany("UserGames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FCG.Domain.Entities.Game", b =>
                {
                    b.Navigation("UserGames");
                });

            modelBuilder.Entity("FCG.Domain.Entities.User", b =>
                {
                    b.Navigation("UserGames");
                });
#pragma warning restore 612, 618
        }
    }
}
