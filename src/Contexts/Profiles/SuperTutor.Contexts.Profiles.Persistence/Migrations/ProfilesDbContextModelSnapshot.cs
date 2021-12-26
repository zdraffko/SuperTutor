﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperTutor.Contexts.Profiles.Persistence;

#nullable disable

namespace SuperTutor.Contexts.Profiles.Persistence.Migrations
{
    [DbContext(typeof(ProfilesDbContext))]
    partial class ProfilesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("profiles")
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.RedactionComment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("CreatedByAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsSettled")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SettledByAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("SettledDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("RedactionComments", "profiles");
                });

            modelBuilder.Entity("SuperTutor.Contexts.Profiles.Domain.Profiles.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid?>("ApprovedByAdminId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RateForOneHour")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TutoringSubject")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("tutoringGrades")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TutoringGrades");

                    b.HasKey("Id");

                    b.ToTable("Profiles", "profiles");
                });

            modelBuilder.Entity("SuperTutor.Contexts.Profiles.Domain.Profiles.Models.Entities.RedactionComments.RedactionComment", b =>
                {
                    b.HasOne("SuperTutor.Contexts.Profiles.Domain.Profiles.Profile", null)
                        .WithMany("RedactionComments")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuperTutor.Contexts.Profiles.Domain.Profiles.Profile", b =>
                {
                    b.Navigation("RedactionComments");
                });
#pragma warning restore 612, 618
        }
    }
}
