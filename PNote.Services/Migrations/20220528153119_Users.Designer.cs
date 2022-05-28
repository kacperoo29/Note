﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PNote.Services;

#nullable disable

namespace PNote.Services.Migrations
{
    [DbContext(typeof(PNoteDbContext))]
    [Migration("20220528153119_Users")]
    partial class Users
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("PNote.Core.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("NoteUserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NoteUserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("PNote.Core.NoteUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PNote.Core.PinnedNote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("NoteId")
                        .HasColumnType("TEXT");

                    b.Property<double>("X")
                        .HasColumnType("REAL");

                    b.Property<double>("Y")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("NoteId")
                        .IsUnique();

                    b.ToTable("PinnedNotes");
                });

            modelBuilder.Entity("PNote.Core.Note", b =>
                {
                    b.HasOne("PNote.Core.NoteUser", null)
                        .WithMany("Notes")
                        .HasForeignKey("NoteUserId");
                });

            modelBuilder.Entity("PNote.Core.PinnedNote", b =>
                {
                    b.HasOne("PNote.Core.Note", "Note")
                        .WithOne("PinnedNote")
                        .HasForeignKey("PNote.Core.PinnedNote", "NoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Note");
                });

            modelBuilder.Entity("PNote.Core.Note", b =>
                {
                    b.Navigation("PinnedNote");
                });

            modelBuilder.Entity("PNote.Core.NoteUser", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
