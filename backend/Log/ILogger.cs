using BankingLedger.Models;
using System.Collections.Generic;

namespace BankingLedger.Log
{
    public interface ILogger
    {
         void logTransaction(transactionType type, int userId, int accountNumber, decimal amount=0); 

         List<Transaction> getTransactionHistory(int userId);
    }
}