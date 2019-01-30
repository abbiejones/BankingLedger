using BankingLedger.DataAccess;
using BankingLedger.Models;
using System;
/// <summary>
/// all business logic related to the user
/// </summary>
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

        /// <summary>
        /// if user exists, check that the password hash matches
        /// if the password matches, set user to current user and return userId
        /// if password is not correct, return 0
        /// if user does not exists, return -1
        /// </summary>
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

        /// <summary>
        /// create new user from user input
        /// encrypt password
        /// create new account for user and associate it with userid
        /// return the accout number on success
        /// return -1 on failure
        /// </summary>
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

        /// <summary>
        /// set current user as an offline form of authorization :) 
        /// set current user in bank account business logic context as well
        /// </summary>
        /// <param name="currentUser"></param>
        public void setUser(User currentUser){
            this._currentUser = new User{
                userId = currentUser.userId,
                firstName = currentUser.firstName,
                lastName = currentUser.lastName
            };
            _bankAccountBiz.setUser(currentUser);
        }

        public void setUser(int userId){
            this._currentUser = new User{
                userId = userId,
                firstName = "",
                lastName = ""
            };
            _bankAccountBiz.setUser(this._currentUser);
        }

        /// <summary>
        /// erase current user when user decides to log off
        /// both in user BL context and bank account BL
        /// </summary>
        public void clearUser(){
            _currentUser = null;
            _bankAccountBiz.clearUser();
        }
    }
}