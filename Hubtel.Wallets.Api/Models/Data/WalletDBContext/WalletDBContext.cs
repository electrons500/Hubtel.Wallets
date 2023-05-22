using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Hubtel.Wallets.Api.Models.Data.WalletDBContext
{
    public partial class WalletDBContext : DbContext
    {
        public WalletDBContext()
        {
        }

        public WalletDBContext(DbContextOptions<WalletDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Wallet> Wallet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Conn");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AccountScheme)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Owner)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
