﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PresMed.Data;

namespace PresMed.Migrations
{
    [DbContext(typeof(BancoContext))]
    partial class BancoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PresMed.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("Complement")
                        .HasColumnType("varchar(40) CHARACTER SET utf8mb4")
                        .HasMaxLength(40);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Crm")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("varchar(40) CHARACTER SET utf8mb4")
                        .HasMaxLength(40);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("Number")
                        .HasColumnType("varchar(7) CHARACTER SET utf8mb4")
                        .HasMaxLength(7);

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PersonType")
                        .HasColumnType("int");

                    b.Property<long?>("Phone")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<string>("Speciality")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("Crm")
                        .IsUnique();

                    b.HasIndex("User")
                        .IsUnique();

                    b.ToTable("Person");
                });

            modelBuilder.Entity("PresMed.Models.Procedures", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(400) CHARACTER SET utf8mb4")
                        .HasMaxLength(400);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Tuss")
                        .IsRequired()
                        .HasColumnType("varchar(400) CHARACTER SET utf8mb4")
                        .HasMaxLength(400);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Tuss")
                        .IsUnique();

                    b.ToTable("Procedure");
                });

            modelBuilder.Entity("PresMed.Models.Scheduling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DayAttendence")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HourAttendence")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("StatusAttendence")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Scheduling");
                });

            modelBuilder.Entity("PresMed.Models.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("FinalHour")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("HourPerDay")
                        .HasColumnType("int");

                    b.Property<DateTime>("InitialHour")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ServiceTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Time");
                });

            modelBuilder.Entity("PresMed.Models.Scheduling", b =>
                {
                    b.HasOne("PresMed.Models.Person", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");

                    b.HasOne("PresMed.Models.Person", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("PresMed.Models.Time", b =>
                {
                    b.HasOne("PresMed.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");
                });
#pragma warning restore 612, 618
        }
    }
}
