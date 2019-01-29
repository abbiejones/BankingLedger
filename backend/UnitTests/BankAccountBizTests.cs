using NUnit.Framework;
using BankingLedger.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BankingLedger.Log;
using System;
using BankingLedger.Biz;
using System.Collections.Generic;
using BankingLedger.Models;
/// <summary>
/// all unit tests related to transactions and bank account actions
/// </summary>
namespace BankingLedger.UnitTests
{
    public class BankAccountBizTests
    {
        public ServiceProvider SetUp(){

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlite()
                .AddSingleton<IBankAccountRepository, BankAccountRepository>()
                .AddSingleton<ITransactionRepository, TransactionRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IUserBiz, UserBiz>()
                .AddSingleton<IBankAccountBiz, BankAccountBiz>()
                .AddSingleton<ILogger,Logger>()
                .AddOptions()
                .AddDbContext<BankingContext>(options => options.UseSqlite("DataSource=:memory:"))
                .BuildServiceProvider();

            return serviceProvider;
        }
        
        public TestingUtility testingUtility(ServiceProvider serviceProvider){
            var util = new TestingUtility(serviceProvider.GetService<IUserBiz>(),
                                        serviceProvider.GetService<IBankAccountBiz>(),
                                        serviceProvider.GetService<BankingContext>(),
                                        serviceProvider.GetService<IUserRepository>(), 
                                        serviceProvider.GetService<IBankAccountRepository>(),
                                        serviceProvider.GetService<ITransactionRepository>());
            return util;
        }

        /// <summary>
        /// create new user
        /// add new account
        /// check balance (should equal zero)
        /// </summary>
        [Test]
        public void balancezero_accountadded(){
            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();
            var firstName = "Joe";
            var lastName = "Green";
            var userName = "joeGreen_1234";
            var password = "a54#a-b";

            util._userBiz.createUser(userName, password, firstName, lastName);
            Assert.AreEqual(new Tuple<int,decimal> (1,0),util._bankAccountBiz.checkBalance());
            util._context.Database.CloseConnection();
            util = null;

        }
        /// <summary>
        /// create new user
        /// deposit money (should return new balance)
        /// </summary>
        [Test]
        public void deposit_from_zero(){
            var serviceProvider = SetUp();           
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();
            var firstName = "Bob";
            var lastName = "Smith";
            var userName = "bobSmith_1234";
            var password = "a54#a-b135";

            util._userBiz.createUser(userName, password, firstName, lastName);
            Assert.AreEqual(util._bankAccountBiz.deposit((decimal)1.34, 1),new Tuple<int,decimal> (1,(decimal)1.34));
            util._context.Database.CloseConnection();   
        }

        /// <summary>
        /// create new user
        /// deposit money (from above initial balance--should return new balance)
        /// </summary>
        [Test]
        public void deposit_from_positive(){
            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();
            var firstName = "Jen";
            var lastName = "Jones";
            var userName = "jenJones_1234";
            var password = "a54#a-b789";

            util._userBiz.createUser(userName, password, firstName, lastName);
            util._bankAccountBiz.deposit((decimal)1.34, 1);
            //Assert.AreEqual(util._bankAccountBiz.deposit((decimal)1.34, 1),new Tuple<int,decimal> (1,(decimal)1.34));
            Assert.AreEqual(util._bankAccountBiz.deposit((decimal)1.01, 1), new Tuple<int,decimal> (1,(decimal)2.35));
            util._context.Database.CloseConnection();
           
        }
        /// <summary>
        /// create new user
        /// withdraw money (should return new balance)
        /// </summary>
        [Test]
        public void withdraw(){
            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();
            var firstName = "Debra";
            var lastName = "Brown";
            var userName = "debraBrown_1234";
            var password = "a54#a-b123";

            util._userBiz.createUser(userName, password, firstName, lastName);
            util._bankAccountBiz.deposit((decimal)1.34, 1);
            util._bankAccountBiz.deposit((decimal)1.01, 1);
            Assert.AreEqual(util._bankAccountBiz.withdraw((decimal).59, 1),new Tuple<int,decimal> (1,(decimal)1.76));
            util._context.Database.CloseConnection();

        }

        /// <summary>
        /// create new user
        /// carry out series of transactions
        /// check that transaction history is as expected
        /// </summary>
        [Test]
        public void transaction_history(){
            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();

            var firstName = "Debra";
            var lastName = "Brown";
            var userName = "debraBrown_1234";
            var password = "a54#a-b123";
            
            util._userBiz.createUser(userName, password, firstName, lastName);

            util._bankAccountBiz.checkBalance(true);
            util._bankAccountBiz.deposit((decimal)1.34, 1);
            util._bankAccountBiz.deposit((decimal)1.01, 1);
            util._bankAccountBiz.withdraw((decimal).59, 1);
            
            List<List<string>> transactionHistory = util._bankAccountBiz.getTransactionHistory();
            List<transactionType> types = new List<transactionType>{transactionType.CREATEACCOUNT, 
                                                                transactionType.CHECKBALANCE,
                                                                transactionType.DEPOSIT,
                                                                transactionType.DEPOSIT,
                                                                transactionType.WITHDRAWAL};

            List<decimal> balanceChanges = new List<decimal>{(decimal)0,(decimal)0,(decimal)1.34,(decimal)1.01,(decimal)0.59};

            for (int i =0; i < transactionHistory.Count; ++i){
                Assert.AreEqual(transactionHistory[i][0],(i + 1).ToString());
                int j = 2;
                if (types[i] == transactionType.WITHDRAWAL || types[i] == transactionType.DEPOSIT){
                    Assert.AreEqual(transactionHistory[i][j],balanceChanges[i].ToString());
                    j += 1;
                }
                Assert.AreEqual(transactionHistory[i][j], types[i].ToString());
            }
        }

    }
}