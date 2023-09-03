using ExchangeRateAggregator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateAggregator.Infrastructure.Persistence.EntityFramework
{
    public class ExchangeRateAggregatorDbContext : DbContext
    {
        public ExchangeRateAggregatorDbContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyRate>(entity =>
            {
                entity.Property(e => e.CurrencyCode).HasMaxLength(4);
                entity.Property(e => e.BuyRate).HasColumnType("decimal(12, 6)");
                entity.Property(e => e.SellRate).HasColumnType("decimal(12, 6)");


                entity
                    .HasOne(c => c.Bank)
                    .WithMany(b => b.CurrencyRates);

            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(64);
                entity.Property(e => e.Source).HasMaxLength(256);


            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
