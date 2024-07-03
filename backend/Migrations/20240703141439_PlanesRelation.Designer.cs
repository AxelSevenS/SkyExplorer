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
    [Migration("20240703141439_PlanesRelation")]
    partial class PlanesRelation
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

                    b.ToTable("activities");
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

                    b.ToTable("flights");
                });

            modelBuilder.Entity("SkyExplorer.Lesson", b =>
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

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("note")
                        .HasAnnotation("Relational:JsonPropertyName", "note");

                    b.HasKey("Id");

                    b.ToTable("lessons");
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("SkyExplorer.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Auth")
                        .HasColumnType("integer")
                        .HasColumnName("authorizations")
                        .HasAnnotation("Relational:JsonPropertyName", "authorizations");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email")
                        .HasAnnotation("Relational:JsonPropertyName", "email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firstname")
                        .HasAnnotation("Relational:JsonPropertyName", "firstname");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lastname")
                        .HasAnnotation("Relational:JsonPropertyName", "lastname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password")
                        .HasAnnotation("Relational:JsonPropertyName", "password");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Auth = 65535,
                            Email = "AdminUser",
                            FirstName = "",
                            LastName = "",
                            Password = "MMs9wIImkG8hnTH6C/v7cyaENECVzczmXzuRN8w1pIk="
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
