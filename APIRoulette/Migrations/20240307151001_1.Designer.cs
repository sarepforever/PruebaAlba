﻿// <auto-generated />
using APIRoulette.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIRoulette.Migrations
{
    [DbContext(typeof(BetDbContext))]
    [Migration("20240307151001_1")]
    partial class _1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APIRoulette.Entities.Bet", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("betNumber")
                        .HasColumnType("int");

                    b.Property<int>("betValue")
                        .HasColumnType("int");

                    b.Property<int>("roulette_Id")
                        .HasColumnType("int");

                    b.Property<bool>("won")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("roulette_Id");

                    b.ToTable("Bet");
                });

            modelBuilder.Entity("APIRoulette.Entities.ConfigBet", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("numMaxRoulette")
                        .HasColumnType("int");

                    b.Property<int>("numMinRoulette")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("ConfigBet");
                });

            modelBuilder.Entity("APIRoulette.Entities.Roulette", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("numMax")
                        .HasColumnType("int");

                    b.Property<int>("numMin")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Roulette");
                });

            modelBuilder.Entity("APIRoulette.Entities.Bet", b =>
                {
                    b.HasOne("APIRoulette.Entities.Roulette", "Roulette")
                        .WithMany("_Bet")
                        .HasForeignKey("roulette_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roulette");
                });

            modelBuilder.Entity("APIRoulette.Entities.Roulette", b =>
                {
                    b.Navigation("_Bet");
                });
#pragma warning restore 612, 618
        }
    }
}
