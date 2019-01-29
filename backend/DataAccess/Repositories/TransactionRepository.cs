using BankingLedger.Models;
using System.Collections.Generic;
using System.Linq;
using System;
/// <summary>
/// all database access actions related to transactions
/// </summary>
namespace BankingLedger.DataAccess
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingContext _context;
        public TransactionRepository(BankingContext context, bool test=false){
            _context = context;
        }
        public List<Transaction> getTransactionHistory(int userId) { 
            try{
            IEnumerable<Transaction> transactionHistory = from trans in _context.Transactions
                                                          where trans.userId.Equals(userId)
                                                          select trans;

            return transactionHistory.ToList();
            } catch {
                return null;
            }

        }
        public bool addTransaction(Transaction newTransaction) { 
         
            _context.Transactions.Add(newTransaction);
            return SaveChanges();
        }
        public bool SaveChanges(){
            try{
                _context.SaveChanges();
                return true;
            } catch {
                return false;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}