using System;
using BankingLedger.Biz;
using System.Collections.Generic;

namespace BankingLedger.Manager
{
    /// <summary>
    /// once logged in, this is the dashboard that the authenticated user sees
    /// </summary>
    public class UserDashboard
    {

        const int MAX_ACCOUNT_NUMBER = 3000; //maximum number of accounts available in bank
        private readonly IUserBiz _userBiz;
        private readonly IBankAccountBiz _bankAccountBiz;
        public UserDashboard(IUserBiz userBiz, IBankAccountBiz bankAccountBiz){
            _userBiz = userBiz;
            _bankAccountBiz = bankAccountBiz;
        }

        /// <summary>
        /// main menu options for authenticated user
        /// </summary>
        /// <returns>choice to banking interface</returns>
        public int menu(){
            Console.WriteLine("\n\nWhat would you like to do?\n");
            Console.WriteLine("Check balance (1)");
            Console.WriteLine("Make a deposit (2)");
            Console.WriteLine("Make a withdrawal (3)");
            Console.WriteLine("View transaction history (4)");
            Console.WriteLine("Logout (5)");
    
            var input = Console.ReadLine();

            return Utility.tryParse(input, 0, 6);
        }
        /// <summary>
        /// kicks off withdrawal workflow
        /// and prints balance after withdrawal to console
        /// </summary>
        /// <returns>success/failure</returns>
        public bool withdraw(){

            bool validAccount = false;
            do {
                Console.WriteLine("Please enter the account number for the account in which you'd like to withdraw money: ");
                var input = Console.ReadLine();

                int accountNumber = Utility.tryParse(input, 0, MAX_ACCOUNT_NUMBER);

                Tuple<int, decimal> accountInfo = _bankAccountBiz.checkBalance(); //account number, balance

                if (accountInfo.Item1 == accountNumber){
                    validAccount = true;
                    bool validAmount = false;

                    do{
                        Console.WriteLine("Please enter the amount you'd like to withdraw: ");
                        input = Console.ReadLine();
                        decimal amount = Utility.tryParseDecimal(input, 0, accountInfo.Item2);

                        if (amount > 0){
                            validAmount = true;
                            Tuple<int, decimal> account = _bankAccountBiz.withdraw(amount, accountNumber); //account number, balance

                        } else {
                            Console.WriteLine("Sorry, that amount is not valid.");
                            Utility.printAccountInfo(accountInfo);
                        }

                    } while (!validAmount);

                } else {
                    Console.WriteLine("Sorry. That account number is not valid.");
                }

            } while (!validAccount);
            return true;    
        }
        /// <summary>
        /// kicks off deposit workflow and
        /// prints balance after deposit to console
        /// </summary>
        /// <returns>success/failures</returns>
        public bool deposit(){
            bool validAccount = false;
            do {
                Console.WriteLine("Please enter the account number for the account in which you'd like to deposit money: ");
                var input = Console.ReadLine();

                int accountNumber = Utility.tryParse(input, 0, MAX_ACCOUNT_NUMBER);
                
                Tuple<int, decimal> accountInfo = _bankAccountBiz.checkBalance();

                if (accountInfo.Item1 == accountNumber){
                    validAccount = true;
                    bool validAmount = false;

                    do{
                        Console.WriteLine("Please enter the amount you'd like to deposit: ");
                        Console.WriteLine("Max deposit amount: $10,000");
                        input = Console.ReadLine();
                        decimal amount = Utility.tryParseDecimal(input, 0, 10000);

                        if (amount > 0){
                            validAmount = true;
                            Tuple<int, decimal> account = _bankAccountBiz.deposit(amount, accountNumber); //account number, balance

                        } else {
                            Console.WriteLine("Sorry, that amount is not valid.");
                            Utility.printAccountInfo(accountInfo);
                        }

                    } while (!validAmount);

                } else {
                    Console.WriteLine("Sorry. That account number is not valid.");
                }

            } while (!validAccount);

            return true;
        }

        /// <summary>
        /// prints transaction history to console
        /// </summary>
        /// <returns>success/failure</returns>
        public bool viewTransactionHistory(){
            List<List<string>> transactions = _bankAccountBiz.getTransactionHistory();
            if (transactions != null){
                foreach (List<string> transaction in transactions){ //loop through transactions, printing information
                    foreach (string trans in transaction){
                        Console.Write("{0} | ", trans);
                    }
                    Console.Write("\n");
                }

                return true;
            } 

            return false;
        }

        /// <summary>
        /// prints account balance to console
        /// </summary>
        /// <returns>success/failure</returns>
        public bool checkBalance(){
            Tuple<int, decimal> account = _bankAccountBiz.checkBalance(true); //account number, balance

            if (account != null){
                Utility.printAccountInfo(account);

                return true;
            }

            return false;
        }
        /// <summary>
        /// removes user's information from current user field upon logging out
        /// </summary>
        public void clearUser(){
            _userBiz.clearUser(); //when logging out, 
        }
    }
}