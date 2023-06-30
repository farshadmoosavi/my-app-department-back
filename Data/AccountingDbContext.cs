using System;
using accounting.Models;
using Microsoft.EntityFrameworkCore;

namespace accounting.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
        {
        }
             public DbSet<Organization> Organizations { get; set; }
             public DbSet<Customer> Customers { get; set; }
             public DbSet<Users> Users { get; set; }
             public DbSet<Currency> Currencies { get; set; }
         }
    }



    //public class AccountingDbContext: DbContext
    //{

    //       public DbSet<Organization> Organizations{ get; set; }
    //       public DbSet<Customer> Customers { get; set; }
    //       public DbSet<Users> Users { get; set; }
    //       public DbSet<Currency> Currencies { get; set; }

    //       private readonly string? _organizationConnectionString;


    //       public AccountingDbContext(DbContextOptions<AccountingDbContext> options, string organizationConnectionString)
    //       : base(options)
    //       {
    //           _organizationConnectionString = organizationConnectionString;
    //       }

    //       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //       {
    //           optionsBuilder.UseSqlServer(_organizationConnectionString);
    //       }

    //   }


