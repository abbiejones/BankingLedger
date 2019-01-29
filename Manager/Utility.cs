using System;

namespace BankingLedger.Manager
{
    public static class Utility
    {
        public static decimal tryParseDecimal(string input, decimal min, decimal max){      
            decimal parsed;
            if (decimal.TryParse(input, out parsed) && (parsed * 100 == Math.Floor(parsed * 100))){
                if (parsed > min && parsed <= max){
                    return parsed;
                }
            }
            return -1;
        }

        public static int tryParse(string input, int min, int max){
            int parsed;
            if (int.TryParse(input, out parsed)){
                if (parsed > min && parsed < max){
                    return parsed;
                }
            }
            return -1;
        }

        public static string hidePassword(){
            string password= "";

            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                
                
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password+= key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password= password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if(key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            return password;
        }   

        public static void printAccountInfo(Tuple<int, decimal> accountInfo){
            Console.WriteLine("Account #{0} balance: ", accountInfo.Item1);
            Console.WriteLine("{0}", accountInfo.Item2.ToString("c2"));
        }
    }
}