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
    [DbContext(typeof(VocabListDbContext))]
    [Migration("20220723222609_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Models.VocabList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("VocabList");
                });

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Models.VocabListItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Term")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("VocabListId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WordType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("VocabListId");

                    b.ToTable("VocabListItem");
                });

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Models.VocabListItem", b =>
                {
                    b.HasOne("GermanVocabApp.DataAccess.EntityFramework.Models.VocabList", "VocabList")
                        .WithMany("ListItems")
                        .HasForeignKey("VocabListId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("VocabList");
                });

            modelBuilder.Entity("GermanVocabApp.DataAccess.EntityFramework.Models.VocabList", b =>
                {
                    b.Navigation("ListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
