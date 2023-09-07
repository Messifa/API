﻿// <auto-generated />
using System;
using DocAppointApi.Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocAppointApi.Migrations
{
    [DbContext(typeof(DbContextRed))]
    partial class DbContextRedModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DocAppointApi.Models.Consecration", b =>
                {
                    b.Property<int>("consId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("consId"));

                    b.Property<DateTime>("consDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("consName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("consPoids")
                        .HasColumnType("integer");

                    b.Property<string>("consTaille")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("consTension")
                        .HasColumnType("integer");

                    b.HasKey("consId");

                    b.ToTable("Consecrations");
                });

            modelBuilder.Entity("DocAppointApi.Models.MalariaCuire", b =>
                {
                    b.Property<int>("malaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("malaid"));

                    b.Property<string>("designationM")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("malaid");

                    b.ToTable("MalariaCuires");
                });

            modelBuilder.Entity("DocAppointApi.Models.RDVM", b =>
                {
                    b.Property<int>("RDVId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RDVId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Datedb")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Datefin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RDVlibelle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RDVId");

                    b.ToTable("RDVMs");
                });

            modelBuilder.Entity("DocAppointApi.Models.Specialite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Specialites");
                });

            modelBuilder.Entity("DocAppointApi.Models.Statut", b =>
                {
                    b.Property<int>("RDVPId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RDVPId"));

                    b.Property<string>("val")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RDVPId");

                    b.ToTable("Statuts");
                });

            modelBuilder.Entity("DocAppointApi.Models.TraitemtP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MedocAvis")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MedocTr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Pid")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TraitemtPs");
                });

            modelBuilder.Entity("DocAppointApi.Models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("userId"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phonenumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("userId");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("DocAppointApi.Models.Adminis", b =>
                {
                    b.HasBaseType("DocAppointApi.Models.User");

                    b.Property<int>("AdminId")
                        .HasColumnType("integer");

                    b.Property<string>("titre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Adminis", (string)null);
                });

            modelBuilder.Entity("DocAppointApi.Models.Medecin", b =>
                {
                    b.HasBaseType("DocAppointApi.Models.User");

                    b.Property<string>("Specialite")
                        .IsRequired()
                        .HasColumnType("text");

                    b.ToTable("Medecins", (string)null);
                });

            modelBuilder.Entity("DocAppointApi.Models.Patient", b =>
                {
                    b.HasBaseType("DocAppointApi.Models.User");

                    b.Property<string>("AdresseP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Sexe")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("malaid")
                        .HasColumnType("integer");

                    b.HasIndex("malaid");

                    b.ToTable("Patients", (string)null);
                });

            modelBuilder.Entity("DocAppointApi.Models.Adminis", b =>
                {
                    b.HasOne("DocAppointApi.Models.User", null)
                        .WithOne()
                        .HasForeignKey("DocAppointApi.Models.Adminis", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocAppointApi.Models.Medecin", b =>
                {
                    b.HasOne("DocAppointApi.Models.User", null)
                        .WithOne()
                        .HasForeignKey("DocAppointApi.Models.Medecin", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocAppointApi.Models.Patient", b =>
                {
                    b.HasOne("DocAppointApi.Models.MalariaCuire", "MalariaCuire")
                        .WithMany()
                        .HasForeignKey("malaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocAppointApi.Models.User", null)
                        .WithOne()
                        .HasForeignKey("DocAppointApi.Models.Patient", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MalariaCuire");
                });
#pragma warning restore 612, 618
        }
    }
}
