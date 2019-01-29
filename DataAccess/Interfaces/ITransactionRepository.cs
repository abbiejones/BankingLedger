using System.Collections.Generic;
using BankingLedger.Models;
using System;

namespace BankingLedger.DataAccess
{
    public interface ITransactionRepository : IDisposable
    {
        List<Transaction> getTransactionHistory(int accountNumber);
        bool addTransaction(Transaction newTransaction);

    }
}