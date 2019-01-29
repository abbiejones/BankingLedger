using BankingLedger.Biz;
using System;

//TODO: make web portal if you have the time and energy to do that
//TODO: exception handling

namespace BankingLedger.Manager
{
    public class BankingInterface
    {
        private readonly IUserBiz _userBiz;
        private readonly IBankAccountBiz _bankAccountBiz;
        public BankingInterface(IUserBiz userBiz, IBankAccountBiz bankAccountBiz){
            _userBiz = userBiz;
            _bankAccountBiz = bankAccountBiz;
        }

        public void application(){
            
            MainMenu mainMenu = new MainMenu(_userBiz, _bankAccountBiz);
            var exit = false;
            var validChoice = false;
            var authenticated = false;

            do{
                do{
                    var choice = mainMenu.welcomeScreen();

                    if (choice == 1) {
                        authenticated = validChoice = mainMenu.login();
                    }

                    else if (choice == 2) {
                        authenticated = validChoice = mainMenu.register();
                    }

                    else if (choice == 3){
                        exit =validChoice = true;
                    }
                    else {
                        Console.WriteLine("Sorry. We didn't understand that.");
                    }
                } while (!validChoice);

                if (authenticated) {
                    UserDashboard userDashboard = new UserDashboard(_userBiz, _bankAccountBiz);
                    bool stop = false;
                    do{
                        var choice = userDashboard.menu();
                        bool transactionComplete;

                        if (choice == 1) {
                            transactionComplete = userDashboard.checkBalance();

                        }

                        else if (choice == 2) {
                            transactionComplete = userDashboard.deposit();

                        }

                        else if (choice == 3){
                            transactionComplete = userDashboard.withdraw();

                        }

                        else if (choice == 4) {
                            transactionComplete = userDashboard.viewTransactionHistory();

                        } else if (choice == 5){
                            authenticated = false; 
                            userDashboard.clearUser();                      
                            stop = true;
                        }
                        else {
                            Console.WriteLine("Sorry. We didn't understand that.");
                        }
                    } while (!stop);
                }
            } while (!exit);
        } 
    } 
}