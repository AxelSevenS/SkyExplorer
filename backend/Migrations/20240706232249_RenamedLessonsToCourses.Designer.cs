﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SkyExplorer;

#nullable disable

namespace SkyExplorer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240706232249_RenamedLessonsToCourses")]
    partial class RenamedLessonsToCourses
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SkyExplorer.Activity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("FlightId")
                        .HasColumnType("bigint")
                        .HasColumnName("flight_id")
                        .HasAnnotation("Relational:JsonPropertyName", "flightId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title")
                        .HasAnnotation("Relational:JsonPropertyName", "title");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("activities");
                });

            modelBuilder.Entity("SkyExplorer.AppUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name")
                        .HasAnnotation("Relational:JsonPropertyName", "firstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name")
                        .HasAnnotation("Relational:JsonPropertyName", "lastName");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("role")
                        .HasAnnotation("Relational:JsonPropertyName", "role");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "admin@sky-explorer.fr",
                            FirstName = "Admin",
                            LastName = "",
                            Password = "MMs9wIImkG8hnTH6C/v7cyaENECVzczmXzuRN8w1pIk=",
                            Role = 3
                        });
                });

            modelBuilder.Entity("SkyExplorer.Bill", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasAnnotation("Relational:JsonPropertyName", "createdAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("url")
                        .HasAnnotation("Relational:JsonPropertyName", "url");

                    b.Property<bool>("WasAcquitted")
                        .HasColumnType("boolean")
                        .HasColumnName("was_acquitted")
                        .HasAnnotation("Relational:JsonPropertyName", "wasAcquitted");

                    b.HasKey("Id");

                    b.ToTable("bills");
                });

            modelBuilder.Entity("SkyExplorer.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AchievedGoals")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("achieved_goals")
                        .HasAnnotation("Relational:JsonPropertyName", "achievedGoals");

                    b.Property<long>("FlightId")
                        .HasColumnType("bigint")
                        .HasColumnName("flight_id")
                        .HasAnnotation("Relational:JsonPropertyName", "flightId");

                    b.Property<string>("Goals")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("goals")
                        .HasAnnotation("Relational:JsonPropertyName", "goals");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("notes")
                        .HasAnnotation("Relational:JsonPropertyName", "notes");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("SkyExplorer.Flight", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BillId")
                        .HasColumnType("bigint")
                        .HasColumnName("bill_id")
                        .HasAnnotation("Relational:JsonPropertyName", "billId");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_time")
                        .HasAnnotation("Relational:JsonPropertyName", "dateTime");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("interval")
                        .HasColumnName("duration")
                        .HasAnnotation("Relational:JsonPropertyName", "duration");

                    b.Property<string>("FlightType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("flight_type")
                        .HasAnnotation("Relational:JsonPropertyName", "flightType");

                    b.Property<long>("OverseerId")
                        .HasColumnType("bigint")
                        .HasColumnName("overseer_id")
                        .HasAnnotation("Relational:JsonPropertyName", "overseerId");

                    b.Property<long>("PlaneId")
                        .HasColumnType("bigint")
                        .HasColumnName("plane_id")
                        .HasAnnotation("Relational:JsonPropertyName", "planeId");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id")
                        .HasAnnotation("Relational:JsonPropertyName", "userId");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("OverseerId");

                    b.HasIndex("PlaneId");

                    b.HasIndex("UserId");

                    b.ToTable("flights");
                });

            modelBuilder.Entity("SkyExplorer.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .HasColumnType("text")
                        .HasColumnName("body")
                        .HasAnnotation("Relational:JsonPropertyName", "body");

                    b.Property<int>("RecipientId")
                        .HasColumnType("integer")
                        .HasColumnName("recipient_id")
                        .HasAnnotation("Relational:JsonPropertyName", "recipientId");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer")
                        .HasColumnName("sender_id")
                        .HasAnnotation("Relational:JsonPropertyName", "senderId");

                    b.Property<DateTime>("SendingDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("sending_date")
                        .HasAnnotation("Relational:JsonPropertyName", "sendingDate");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title")
                        .HasAnnotation("Relational:JsonPropertyName", "title");

                    b.HasKey("Id");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("SkyExplorer.Plane", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name")
                        .HasAnnotation("Relational:JsonPropertyName", "name");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status")
                        .HasAnnotation("Relational:JsonPropertyName", "status");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type")
                        .HasAnnotation("Relational:JsonPropertyName", "type");

                    b.HasKey("Id");

                    b.ToTable("planes");
                });

            modelBuilder.Entity("SkyExplorer.Activity", b =>
                {
                    b.HasOne("SkyExplorer.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("SkyExplorer.Course", b =>
                {
                    b.HasOne("SkyExplorer.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flight");
                });

            modelBuilder.Entity("SkyExplorer.Flight", b =>
                {
                    b.HasOne("SkyExplorer.Bill", "Bill")
                        .WithMany()
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkyExplorer.AppUser", "Overseer")
                        .WithMany()
                        .HasForeignKey("OverseerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkyExplorer.Plane", "Plane")
                        .WithMany()
                        .HasForeignKey("PlaneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SkyExplorer.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Overseer");

                    b.Navigation("Plane");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
