using BankingLedger.Models;
using BankingLedger.DataAccess;
using System.Collections.Generic;

namespace BankingLedger.Log
{


    public class Logger : ILogger
    {
        private readonly ITransactionRepository _transactionRepository;
        public Logger(ITransactionRepository transactionRepository){
            _transactionRepository = transactionRepository;
        }
        public void logTransaction(transactionType type, int userId, int accountNumber, decimal amount = 0 ){
            Transaction newTransaction = new Transaction{
                type = type,
                userId = userId,
                accountNumber = accountNumber,
                amount = amount
            };
            _transactionRepository.addTransaction(newTransaction);
        }

        public List<Transaction> getTransactionHistory(int userId){
            return _transactionRepository.getTransactionHistory(userId);
        }
    }
}