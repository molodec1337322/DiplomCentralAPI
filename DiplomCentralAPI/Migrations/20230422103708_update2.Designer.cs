﻿// <auto-generated />
using System;
using DiplomCentralAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiplomCentralAPI.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20230422103708_update2")]
    partial class update2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("HandlerId")
                        .HasColumnType("integer");

                    b.Property<string>("ResultPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SchemaId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VideoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("HandlerId");

                    b.HasIndex("SchemaId");

                    b.ToTable("Experiments");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Handler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ScriptPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Handlers");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExperimentId")
                        .HasColumnType("integer");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Schema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VideoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Schemas");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Experiment", b =>
                {
                    b.HasOne("DiplomCentralAPI.Data.Models.Handler", "Handler")
                        .WithMany("Experiments")
                        .HasForeignKey("HandlerId");

                    b.HasOne("DiplomCentralAPI.Data.Models.Schema", "Schema")
                        .WithMany("Experiments")
                        .HasForeignKey("SchemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Handler");

                    b.Navigation("Schema");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Photo", b =>
                {
                    b.HasOne("DiplomCentralAPI.Data.Models.Experiment", "Experiment")
                        .WithMany("Photos")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Experiment");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Experiment", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Handler", b =>
                {
                    b.Navigation("Experiments");
                });

            modelBuilder.Entity("DiplomCentralAPI.Data.Models.Schema", b =>
                {
                    b.Navigation("Experiments");
                });
#pragma warning restore 612, 618
        }
    }
}
