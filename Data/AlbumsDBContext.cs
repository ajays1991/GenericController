using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Data.entities;

namespace Data
{
    public partial class AlbumsDBContext : DbContext, IAlbumsDBContext
    {
        public AlbumsDBContext(DbContextOptions<AlbumsDBContext> options) : base(options)
        {

        }
        //public AlbumDBContext()
        //{
        //}

        //public AlbumDBContext(DbContextOptions<AlbumDBContext> options)
        //    : base(options)
        //{
        //}

        public virtual DbSet<Albums> Albums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AlbumsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Albums>(entity =>
            {
                //entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Id).HasMaxLength(4);
                entity.Property(e => e.Artist)
                    .IsRequired()
                    .HasColumnName("artist")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnName("genre")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
