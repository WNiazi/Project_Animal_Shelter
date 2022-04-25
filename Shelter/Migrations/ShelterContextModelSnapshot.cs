﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shelter.Models;

namespace Shelter.Migrations
{
    [DbContext(typeof(ShelterContext))]
    partial class ShelterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Shelter.Models.Cat", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CatId");

                    b.ToTable("Cats");

                    b.HasData(
                        new
                        {
                            CatId = 1,
                            Age = 1,
                            Gender = "Male",
                            Name = "Bob",
                            Type = "furry"
                        },
                        new
                        {
                            CatId = 2,
                            Age = 2,
                            Gender = "Female",
                            Name = "Tod",
                            Type = "very furry"
                        },
                        new
                        {
                            CatId = 3,
                            Age = 3,
                            Gender = "Male",
                            Name = "Sod",
                            Type = "not furry"
                        });
                });

            modelBuilder.Entity("Shelter.Models.Dog", b =>
                {
                    b.Property<int>("DogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("DogId");

                    b.ToTable("Dogs");

                    b.HasData(
                        new
                        {
                            DogId = 1,
                            Age = 4,
                            Gender = "Female",
                            Name = "Cat",
                            Type = "not at all furry"
                        },
                        new
                        {
                            DogId = 2,
                            Age = 5,
                            Gender = "Male",
                            Name = "Mat",
                            Type = "a little fur"
                        },
                        new
                        {
                            DogId = 3,
                            Age = 6,
                            Gender = "Female",
                            Name = "Sat",
                            Type = "bald"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
