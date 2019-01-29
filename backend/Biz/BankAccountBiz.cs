using BankingLedger.DataAccess;
using BankingLedger.Models;
using System;
using System.Collections.Generic;
using BankingLedger.Log;

/// <summary>
/// all business logic related to bank account actions
/// </summary>
namespace BankingLedger.Biz
{
    public class BankAccountBiz : IBankAccountBiz
    {
        private User _currentUser;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ILogger _logger;
        public BankAccountBiz(IBankAccountRepository bankAccountRepository, ILogger logger){
            _bankAccountRepository = bankAccountRepository;
            _logger = logger;

        } 
        /// <summary>
        /// check balance on bank account associated with current user
        /// log transaction
        /// </summary>
        /// <param name="userCheck"></param>
        /// <returns></returns>
        public Tuple<int, decimal> checkBalance(bool userCheck=false){
            BankAccount account = _bankAccountRepository.checkBalance(_currentUser.userId);


            if (account != null){
                if (userCheck){
                _logger.logTransaction(transactionType.CHECKBALANCE,_currentUser.userId, account.accountNumber);
                }
                return new Tuple<int,decimal>(account.accountNumber, account.balance); 
            }   
            return null;      
        }

        /// <summary>
        /// withdraw specified amount from bank account associated with current user
        /// log transaction
        /// </summary>
        /// <param name="amountToWithdraw"></param>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public Tuple<int, decimal> withdraw(decimal amountToWithdraw, int accountNumber){
            BankAccount accountToWithdraw = _bankAccountRepository.withdraw(amountToWithdraw, accountNumber, _currentUser.userId);

            if (accountToWithdraw != null){
                _logger.logTransaction(transactionType.WITHDRAWAL,_currentUser.userId, accountToWithdraw.accountNumber, amountToWithdraw);
                return new Tuple<int, decimal>(accountToWithdraw.accountNumber, accountToWithdraw.balance);
            }

            return null;
        }

        /// <summary>
        /// deposit specified amount from bank account associated with current user
        /// </summary>
        /// <param name="amountToDeposit"></param>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public Tuple<int, decimal> deposit(decimal amountToDeposit, int accountNumber){
            BankAccount accountToDeposit = _bankAccountRepository.deposit(amountToDeposit, accountNumber, _currentUser.userId);

            if (accountToDeposit != null){
                _logger.logTransaction(transactionType.DEPOSIT,_currentUser.userId, accountToDeposit.accountNumber, amountToDeposit);
                return new Tuple<int, decimal>(accountToDeposit.accountNumber, accountToDeposit.balance);
            }

            return null;
        }

        /// <summary>
        /// create new account associated with current user
        /// </summary>
        /// <returns></returns>
        public int addAccount(){
            var newBankAccount = new BankAccount{
                userId = _currentUser.userId,
                balance = 0
            };

            _bankAccountRepository.addAccount(newBankAccount);      

            var account = _bankAccountRepository.getAccountNumber(_currentUser.userId);
            _logger.logTransaction(transactionType.CREATEACCOUNT,_currentUser.userId, account.accountNumber);

            return account.accountNumber;
        }

        /// <summary>
        /// retrieve all transactions associated with current user
        /// </summary>
        /// <returns></returns>
        public List<List<string>> getTransactionHistory(){
            
            List<Transaction> transactions = _logger.getTransactionHistory(_currentUser.userId);
            if (transactions != null){
                List<List<string>> transactionStrings = new List<List<string>>();
                foreach (Transaction transaction in transactions){
                    List<string> singleTransaction = new List<string>();
                    singleTransaction.Add(transaction.transactionNo.ToString());
                    singleTransaction.Add(transaction.transactionTime.ToString());
                    if (transaction.type == transactionType.WITHDRAWAL|| transaction.type == transactionType.DEPOSIT){
                        singleTransaction.Add(transaction.amount.ToString());
                    }
                    singleTransaction.Add(transaction.type.ToString());
        
                    transactionStrings.Add(singleTransaction);
                }
                return transactionStrings;
            }

            return null;

        }
        /// <summary>
        /// set current user (as a form of offline authorization)
        /// </summary>
        /// <param name="currentUser"></param>
        public void setUser(User currentUser){
            _currentUser = new User{
                userId = currentUser.userId,
                firstName = currentUser.firstName,
                lastName = currentUser.lastName
            };
        }

        /// <summary>
        /// reset current user upon logging off
        /// </summary>
        public void clearUser(){
            _currentUser = null;
        }
      
    }
}