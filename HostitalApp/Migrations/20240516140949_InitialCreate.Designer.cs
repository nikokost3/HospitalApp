﻿// <auto-generated />
using System;
using HospitalApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HospitalApp.Migrations
{
    [DbContext(typeof(UsersDoctorsPatientDbContext))]
    [Migration("20240516140949_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HostitalApp.Data.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int")
                        .HasColumnName("DOCTOR_ID");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("APPOINTMENT", (string)null);
                });

            modelBuilder.Entity("HostitalApp.Data.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Clinic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CLINIC");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("DOCTORS", (string)null);
                });

            modelBuilder.Entity("HostitalApp.Data.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Clinic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CLINIC");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PATIENTS", (string)null);
                });

            modelBuilder.Entity("HostitalApp.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Firstname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FIRSTNAME");

                    b.Property<string>("Lastname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LASTNAME");

                    b.Property<string>("Password")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("USER_ROLE");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Lastname" }, "IX_LASTNAME");

                    b.HasIndex(new[] { "Email" }, "UQ_EMAIL")
                        .IsUnique()
                        .HasFilter("[EMAIL] IS NOT NULL");

                    b.HasIndex(new[] { "Username" }, "UQ_USERNAME")
                        .IsUnique()
                        .HasFilter("[USERNAME] IS NOT NULL");

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("PATIENTS_APPOINTMENTS", b =>
                {
                    b.Property<int>("AppointmentsId")
                        .HasColumnType("int");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentsId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("PATIENTS_APPOINTMENTS");
                });

            modelBuilder.Entity("HostitalApp.Data.Appointment", b =>
                {
                    b.HasOne("HostitalApp.Data.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_DOCTOR_APPOINTMENT");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("HostitalApp.Data.Doctor", b =>
                {
                    b.HasOne("HostitalApp.Data.User", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("HostitalApp.Data.Doctor", "UserId")
                        .HasConstraintName("FK_DOCTORS_USERS");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HostitalApp.Data.Patient", b =>
                {
                    b.HasOne("HostitalApp.Data.User", "User")
                        .WithOne("Patient")
                        .HasForeignKey("HostitalApp.Data.Patient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PATIENTS_USERS");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PATIENTS_APPOINTMENTS", b =>
                {
                    b.HasOne("HostitalApp.Data.Appointment", null)
                        .WithMany()
                        .HasForeignKey("AppointmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HostitalApp.Data.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HostitalApp.Data.Doctor", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("HostitalApp.Data.User", b =>
                {
                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
