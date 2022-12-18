﻿// <auto-generated />
using System;
using GermanVocabApp.DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GermanVocabApp.DataAccess.EntityFramework.Migrations
{
    [DbContext(typeof(GermanAppAppDbContext))]
    [Migration("20221218162847_InitialUsers")]
    partial class InitialUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Authentication.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Vocab.Models.VocabList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("VocabList");
                });

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Vocab.Models.VocabListItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuxiliaryVerb")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comparative")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("English")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FixedPlurality")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("German")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("IsWeakMasculineNoun")
                        .HasColumnType("bit");

                    b.Property<string>("Perfect")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Plural")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Preposition")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("PrepositionCase")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ReflexiveCase")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Separability")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Superlative")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ThirdPersonImperfect")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("ThirdPersonPresent")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Transitivity")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VocabListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WordType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("VocabListId");

                    b.ToTable("VocabListItem");
                });

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Vocab.Models.VocabListItem", b =>
                {
                    b.HasOne("GermanVocabApp.DataAccess.EntityFramework.Vocab.Models.VocabList", "VocabList")
                        .WithMany("ListItems")
                        .HasForeignKey("VocabListId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("VocabList");
                });

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Vocab.Models.VocabList", b =>
                {
                    b.Navigation("ListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
