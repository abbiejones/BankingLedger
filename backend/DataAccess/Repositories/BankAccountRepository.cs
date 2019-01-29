using BankingLedger.Models;
using System.Linq;
using System;
/// <summary>
/// all database access functions related to bank account actions
/// </summary>
namespace BankingLedger.DataAccess
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly BankingContext _context;
        public BankAccountRepository(BankingContext context, bool test=false){
        
            _context = context;
        
        }

        public BankAccount getAccountNumber(int userId){
             return _context.BankAccounts.Where(x => x.userId.Equals(userId)).FirstOrDefault();
        }
        public int addAccount(BankAccount newAccount){
            
            _context.BankAccounts.Add(newAccount);
            SaveChanges();
            return 1;
            
        }
        public BankAccount checkBalance(int userId){
            try{
            BankAccount account = _context.BankAccounts
                                  .Where(x => x.userId.Equals(userId)).FirstOrDefault();
            
            return account;
            } catch {
                return null;
            }
        }
        public BankAccount deposit(decimal amount, int accountNumber, int userId){
            try{
                BankAccount account = _context.BankAccounts
                                  .Where(x => x.accountNumber.Equals(accountNumber))
                                  .FirstOrDefault<BankAccount>();
            
                account.balance = account.balance + amount;
                SaveChanges();
                return account;

            } catch {
                return null;
            }
            
            
        }
        public BankAccount withdraw(decimal amount, int accountNumber, int userId){
            try{
                BankAccount account = _context.BankAccounts.FirstOrDefault(x => x.accountNumber.Equals(accountNumber) && x.userId.Equals(userId));
                
                account.balance -= amount;
                SaveChanges();
                return account;
            } catch {
                return null;
            }
        
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public bool SaveChanges(){
            try{
            _context.SaveChanges();
            return true;
            } catch {
                return false;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}