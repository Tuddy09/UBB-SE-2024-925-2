﻿// <auto-generated />
using System;
using BACKEND_925_2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BACKEND_925_2.Migrations
{
    [DbContext(typeof(GamesDbContext))]
    partial class GamesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TwoPlayerGames.Domain.DatabaseObjects.GameStats", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("EloRating")
                        .HasColumnType("int");

                    b.Property<int>("HighestElo")
                        .HasColumnType("int");

                    b.Property<int>("TotalDraws")
                        .HasColumnType("int");

                    b.Property<int>("TotalMatches")
                        .HasColumnType("int");

                    b.Property<int>("TotalNumberOfTurn")
                        .HasColumnType("int");

                    b.Property<int>("TotalPlayTime")
                        .HasColumnType("int");

                    b.Property<int>("TotalWins")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("GameStats");
                });

            modelBuilder.Entity("TwoPlayerGames.Domain.DatabaseObjects.PlayerQueue", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ChessMode")
                        .HasColumnType("int");

                    b.Property<int>("EloRating")
                        .HasColumnType("int");

                    b.Property<int?>("ObstractionHeight")
                        .HasColumnType("int");

                    b.Property<int?>("ObstractionWidth")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("PlayerQueue");
                });

            modelBuilder.Entity("TwoPlayerGames.GameState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StateJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimePlayed")
                        .HasColumnType("int");

                    b.Property<int>("Turn")
                        .HasColumnType("int");

                    b.Property<Guid?>("WinnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameTypeId");

                    b.HasIndex("WinnerId");

                    b.ToTable("GameStates");
                });

            modelBuilder.Entity("TwoPlayerGames.Games", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TwoPlayerGames.Player2PlayerGame", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GameStateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameStateId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("TwoPlayerGames.Domain.DatabaseObjects.GameStats", b =>
                {
                    b.HasOne("TwoPlayerGames.Games", "Game")
                        .WithMany("GameStats")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TwoPlayerGames.Player2PlayerGame", "Player")
                        .WithMany("GameStats")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("TwoPlayerGames.Domain.DatabaseObjects.PlayerQueue", b =>
                {
                    b.HasOne("TwoPlayerGames.Games", "GameType")
                        .WithMany("Queues")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TwoPlayerGames.Player2PlayerGame", "Player")
                        .WithMany("Queues")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameType");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("TwoPlayerGames.GameState", b =>
                {
                    b.HasOne("TwoPlayerGames.Games", "GameType")
                        .WithMany()
                        .HasForeignKey("GameTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TwoPlayerGames.Player2PlayerGame", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId");

                    b.Navigation("GameType");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("TwoPlayerGames.Player2PlayerGame", b =>
                {
                    b.HasOne("TwoPlayerGames.GameState", null)
                        .WithMany("Players")
                        .HasForeignKey("GameStateId");
                });

            modelBuilder.Entity("TwoPlayerGames.GameState", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("TwoPlayerGames.Games", b =>
                {
                    b.Navigation("GameStats");

                    b.Navigation("Queues");
                });

            modelBuilder.Entity("TwoPlayerGames.Player2PlayerGame", b =>
                {
                    b.Navigation("GameStats");

                    b.Navigation("Queues");
                });
#pragma warning restore 612, 618
        }
    }
}
