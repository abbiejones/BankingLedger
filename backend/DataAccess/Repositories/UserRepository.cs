using BankingLedger.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
/// <summary>
/// all database access functions related to user
/// </summary>
namespace BankingLedger.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly BankingContext _context;

        public UserRepository(BankingContext context){

            _context = context;
        }

        public User getUser(string userName){
            try {
                User currentUser = _context.Users
                                .Where(x => x.userName.Equals(userName))
                                .FirstOrDefault();
                return currentUser;
            } catch {
                return null;
            }
            
        }

        public bool createUser(User newUser) {
            try{
                _context.Users.Add(newUser);
                return SaveChanges();

            } catch {
                return false;
            }

        }
        public User verifyUser(string userName, string password) {
            try{
                User currentUser = _context.Users
                                .Where(x => x.userName.Equals(userName))
                                .FirstOrDefault();
                return currentUser;
            } catch {
                return null;
            }

        }

        public bool SaveChanges(){
            try{
                _context.SaveChanges();
                return true;
            } catch {
                return false;
            }
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}