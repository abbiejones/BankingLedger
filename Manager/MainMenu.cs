using System;
using BankingLedger.Biz;

namespace BankingLedger.Manager
{

    public class MainMenu
    {
        private readonly IUserBiz _userBiz;
        private readonly IBankAccountBiz _bankAccountBiz;
        public MainMenu(IUserBiz userBiz, IBankAccountBiz bankAccountBiz){
            _userBiz = userBiz;
            _bankAccountBiz = bankAccountBiz;
        }
        public int welcomeScreen(){
            Console.WriteLine("\nPlease choose from the following options:");
            Console.WriteLine("Log In (1)");
            Console.WriteLine("Register (2)");
            Console.WriteLine("Exit (3)");
            
            var input = Console.ReadLine();

            return Utility.tryParse(input, 0, 4);
        }



        public bool register(){
            Console.WriteLine("Please enter your username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            
            bool matching = false;
            var password = "";

            do{
                password = Utility.hidePassword();
                Console.WriteLine("\nPlease re-enter your password: ");
                var passwordRedo = Utility.hidePassword();

                if (password.Equals(passwordRedo)) {
                    matching = true;
                } else {
                    Console.WriteLine("\nPasswords do not match. Please enter a password: ");
                }
            }

            while(!matching);

            Console.WriteLine("\nPlease enter your first name: ");
            var firstName = Console.ReadLine();

            Console.WriteLine("Please enter your last name: ");
            var lastName = Console.ReadLine();

            if (_userBiz.createUser(username, password,firstName, lastName) >= 1){                     
                return true;                   
            }

            Console.WriteLine("An account with that username already exists!");
            return false;
        }

        public bool login(){
            Console.WriteLine("\nEnter your username:");
            var userName = Console.ReadLine();

            Console.WriteLine("Enter your password:");
            var password = Utility.hidePassword();

            int isValidUser = _userBiz.userExists(userName, password);
            
            if (isValidUser >= 1){
                return true;
            } else if (isValidUser == 0) {
                Console.WriteLine("\nUser exists but password is incorrect");
            } else {
                Console.WriteLine("\nUser does not exist");
            }

            return false;
        }  
    }
}