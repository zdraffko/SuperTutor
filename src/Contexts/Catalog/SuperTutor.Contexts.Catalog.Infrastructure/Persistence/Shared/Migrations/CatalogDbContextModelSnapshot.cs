﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperTutor.Contexts.Catalog.Infrastructure.Persistence.Shared;

#nullable disable

namespace SuperTutor.Contexts.Catalog.Persistence.Shared.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("catalog")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SuperTutor.Contexts.Catalog.Domain.Students.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Students", "catalog");
                });

            modelBuilder.Entity("SuperTutor.Contexts.Catalog.Domain.TutorProfiles.TutorProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("RateForOneHour")
                        .HasPrecision(19, 4)
                        .HasColumnType("decimal(19,4)");

                    b.HasKey("Id");

                    b.ToTable("TutorProfiles", "catalog");
                });

            modelBuilder.Entity("SuperTutor.Contexts.Catalog.Domain.Students.Student", b =>
                {
                    b.OwnsMany("SuperTutor.Contexts.Catalog.Domain.Students.Models.ValueObjects.FavoriteFilters.FavoriteFilter", "FavoriteFilters", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<string>("Filter")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("StudentId", "Id");

                            b1.ToTable("FavoriteFilters", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("FavoriteFilters");
                });

            modelBuilder.Entity("SuperTutor.Contexts.Catalog.Domain.TutorProfiles.TutorProfile", b =>
                {
                    b.OwnsMany("SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects.TutoringGrade", "TutoringGrades", b1 =>
                        {
                            b1.Property<Guid>("TutorProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("TutorProfileId", "Id");

                            b1.ToTable("TutorProfileTutoringGrades", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("TutorProfileId");
                        });

                    b.OwnsOne("SuperTutor.Contexts.Catalog.Domain.TutorProfiles.Models.ValueObjects.TutoringSubject", "TutoringSubject", b1 =>
                        {
                            b1.Property<Guid>("TutorProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("TutorProfileId");

                            b1.ToTable("TutorProfiles", "catalog");

                            b1.WithOwner()
                                .HasForeignKey("TutorProfileId");
                        });

                    b.Navigation("TutoringGrades");

                    b.Navigation("TutoringSubject")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}