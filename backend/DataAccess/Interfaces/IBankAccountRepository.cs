using BankingLedger.Models;
using System;

namespace BankingLedger.DataAccess
{
    public interface IBankAccountRepository : IDisposable
    {
        int addAccount(BankAccount newAccount);
        BankAccount  checkBalance(int userId);
        BankAccount deposit(decimal amount, int accountNumber, int userId);
        BankAccount withdraw(decimal amount, int accountNumber, int userId);
        BankAccount getAccountNumber(int userId);

    }
}