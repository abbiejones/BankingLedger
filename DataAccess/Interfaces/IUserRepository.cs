using BankingLedger.Models;
using System;

namespace BankingLedger.DataAccess
{
    public interface IUserRepository : IDisposable
    {
        bool createUser(User newUser);
        User verifyUser(string userName, string password);
        User getUser(string userName);
        
    }
}