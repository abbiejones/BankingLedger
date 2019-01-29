using System.Collections.Generic;
using System;
using BankingLedger.Models;

namespace BankingLedger.Biz
{
    public interface IBankAccountBiz
    {
        int addAccount();

        Tuple<int, decimal> checkBalance(bool userCheck = false);

        Tuple<int, decimal> withdraw(decimal amountToWithdraw, int accountNumber);

        Tuple<int, decimal> deposit(decimal amountToDeposit, int accountNumber);

        List<List<string>> getTransactionHistory();

        void setUser(User currentUser);

        void clearUser();
    }
}