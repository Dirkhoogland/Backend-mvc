using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend_mvc.Models.Database
{
    public partial class SchoolContext : DbContext
    {

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lijstentable> Lijstentable { get; set; } = null!;
        public virtual DbSet<Tasks> Tasks { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lijstentable>(entity =>
            {
                entity.HasKey(e => e.IdLijst);

                entity.HasIndex(e => e.NaamLijst, "AK_Lijstentable_NaamLijst")
                    .IsUnique();

                entity.HasIndex(e => e.NaamLijst, "IX_Lijstentable")
                    .IsUnique();

                entity.Property(e => e.NaamLijst)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.HasIndex(e => e.Lijst, "IX_Tasks_Lijst");

                entity.Property(e => e.Beschrijving)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Lijst)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Naam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.LijstNavigation)
                    .WithMany(p => p.Tasks)
                    .HasPrincipalKey(p => p.NaamLijst)
                    .HasForeignKey(d => d.Lijst)
                    .HasConstraintName("FK_Tasks_Lijstentable");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
