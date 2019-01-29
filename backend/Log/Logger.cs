using BankingLedger.Models;
using BankingLedger.DataAccess;
using System.Collections.Generic;
/// <summary>
/// logging class for creating and retrieving user transactions
/// </summary>
namespace BankingLedger.Log
{


    public class Logger : ILogger
    {
        private readonly ITransactionRepository _transactionRepository;
        public Logger(ITransactionRepository transactionRepository){
            _transactionRepository = transactionRepository;
        }

        /// <summary>
        /// create new transaction
        /// </summary>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        public void logTransaction(transactionType type, int userId, int accountNumber, decimal amount = 0 ){
            Transaction newTransaction = new Transaction{
                type = type,
                userId = userId,
                accountNumber = accountNumber,
                amount = amount
            };
            _transactionRepository.addTransaction(newTransaction);
        }

        /// <summary>
        /// retrieve all transactions for specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>list of transactions</returns>
        public List<Transaction> getTransactionHistory(int userId){
            return _transactionRepository.getTransactionHistory(userId);
        }
    }
}