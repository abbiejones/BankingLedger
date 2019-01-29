using BankingLedger.DataAccess;
using BankingLedger.Models;
using System;

namespace BankingLedger.Biz
{
    public class UserBiz : IUserBiz
    {
        private readonly IUserRepository _userRepository;
        private readonly IBankAccountBiz _bankAccountBiz;
        private User _currentUser;
        public UserBiz(IUserRepository userRepository, IBankAccountBiz bankAccountBiz){
            _userRepository = userRepository;
            _bankAccountBiz = bankAccountBiz;
        }
        public int userExists(string userName, string password){
            
            User currentUser = _userRepository.verifyUser(userName, password);

            if (currentUser != null){
                if (PasswordHasher.retrievePassword(password, currentUser.passwordSalt, currentUser.passwordHash)){
                    setUser(currentUser);
                    return currentUser.userId;
                } else {
                    return 0;
                }
            }
            
            return -1;
            
        }

        public int createUser(string userName, string password, string firstName, string lastName){
            Tuple<byte[], string> encryptedPw= PasswordHasher.hashPassword(password);

            User newUser = new User{
                firstName = firstName,
                lastName = lastName,
                userName = userName,
                passwordSalt = encryptedPw.Item1,
                passwordHash = encryptedPw.Item2
            };
             if( _userRepository.createUser(newUser)) {
                 setUser(_userRepository.getUser(userName));
                 var accountNumber = _bankAccountBiz.addAccount();
                 
                 return accountNumber;
             }
            return -1;
        }

        private void setUser(User currentUser){
            _currentUser = new User{
                userId = currentUser.userId,
                firstName = currentUser.firstName,
                lastName = currentUser.lastName
            };
            _bankAccountBiz.setUser(currentUser);
        }

        public void clearUser(){
            _currentUser = null;
            _bankAccountBiz.clearUser();
        }
    }
}