using System;
using Line.Models;
using Microsoft.EntityFrameworkCore;

namespace Line.Data
{
    public class LineDbContext : DbContext
    {
        public LineDbContext(DbContextOptions<LineDbContext> options) : base(options)
        {
        
        }
             public DbSet<Transaction> Transactions { get; set; }
             public DbSet<TransactionType> TransactionTypes { get; set; }
             public DbSet<TransactionState> TransactionStates { get; set; }
             public DbSet<User> Users { get; set; }
             public DbSet<Currency> Currencies { get; set; }
             public DbSet<Document> Documents { get; set; }
             public DbSet<Admin> Admins { get; set; }
             public DbSet<SellBuyPrice> SellBuyPrices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Configure the one - to - many relationship between User and Document
            modelBuilder.Entity<User>()
                .HasMany(o => o.Documents)
                .WithOne(u => u.Users)
                .HasForeignKey(u => u.UserId);

            //Configure the one - to - many relationship between TransactionType and Transaction
            modelBuilder.Entity<TransactionType>()
                .HasMany(o => o.Transactions)
                .WithOne(u => u.TransactionTypes)
                .HasForeignKey(u => u.TransactionTypeId);

            //Configure the one - to - many relationship between TransactionState and Transaction
            modelBuilder.Entity<TransactionState>()
                .HasMany(o => o.Transactions)
                .WithOne(u => u.TransactionStates)
                .HasForeignKey(u => u.TransactionStateId);

            //Configure the one - to - many relationship between Currency and SellBuyPrice entity
            modelBuilder.Entity<Currency>()
                .HasMany(o => o.SellBuyPrices)
                .WithOne(u => u.currencies)
                .HasForeignKey(u => u.currencyId);

        }
    }
}




