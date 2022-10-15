﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonalSite.Persistence;

#nullable disable

namespace PersonalSite.Persistence.Npgsql.Migrations
{
    [DbContext(typeof(PersonalSiteDbContext))]
    partial class PersonalSiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonalSite.Domain.Model.JobExperienceAggregate.JobExperience", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("TechStack")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("JobExperiences");
                });

            modelBuilder.Entity("PersonalSite.Persistence.Events.IntegrationEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasPrecision(5)
                        .HasColumnType("timestamp(5) with time zone");

                    b.Property<string>("FullyQualifiedTypeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("boolean");

                    b.Property<string>("SerializedData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IsPublished");

                    b.ToTable("IntegrationEvents");
                });

            modelBuilder.Entity("PersonalSite.Domain.Model.JobExperienceAggregate.JobExperience", b =>
                {
                    b.OwnsOne("PersonalSite.Domain.Model.JobExperienceAggregate.JobPeriod", "JobPeriod", b1 =>
                        {
                            b1.Property<int>("JobExperienceId")
                                .HasColumnType("integer");

                            b1.Property<DateOnly?>("End")
                                .HasColumnType("date");

                            b1.Property<DateOnly>("Start")
                                .HasColumnType("date");

                            b1.HasKey("JobExperienceId");

                            b1.ToTable("JobExperiences");

                            b1.WithOwner()
                                .HasForeignKey("JobExperienceId");
                        });

                    b.Navigation("JobPeriod");
                });
#pragma warning restore 612, 618
        }
    }
}
