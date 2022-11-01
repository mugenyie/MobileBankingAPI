using Microsoft.EntityFrameworkCore;
using MobileBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileBanking.Data
{
    public class MBDbContext : DbContext
    {
        public MBDbContext(DbContextOptions<MBDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(s => s.AccountNumber)
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("MOBILE_BANKING_DB_CON"));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<TransactionLog> TransactionLogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<LogData> LogData { get; set; }
    }
}
