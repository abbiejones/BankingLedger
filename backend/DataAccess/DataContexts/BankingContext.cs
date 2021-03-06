using BankingLedger.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Entity Framework Database context for all banking actions
/// </summary>
namespace BankingLedger.DataAccess
{
    public class BankingContext : DbContext
    {   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(c => c.userId);
            modelBuilder.Entity<User>()
                .Property(c => c.userId)
                .ValueGeneratedOnAdd(); //user id generated on creation
            modelBuilder.Entity<User>()
                .HasAlternateKey(c => c.userName);
            modelBuilder.Entity<User>()
                .HasKey(c => c.userId);

            modelBuilder.Entity<Transaction>()
                .Property(c => c.transactionNo)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Transaction>()
                .Property(c => c.transactionTime)
                .HasDefaultValueSql("datetime('now')"); //grabs current time

            modelBuilder.Entity<BankAccount>()
                .Property(c => c.accountNumber)
                .ValueGeneratedOnAdd();
                
        }
        
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=bank.db");
        
        public BankingContext(DbContextOptions<BankingContext> options) :base(options){}

        public BankingContext(){}
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<BankAccount> BankAccounts {get; set;}
        public virtual DbSet<Transaction> Transactions {get; set;}
        
    }
}