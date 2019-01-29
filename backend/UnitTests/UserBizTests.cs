using NUnit.Framework;
using BankingLedger.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BankingLedger.Log;
using BankingLedger.Biz;

namespace BankingLedger.UnitTests
{
/// <summary>
/// Testing all business logic related to user
/// 
/// </summary>
    [TestFixture]
    public class UserBizTests
    {
        /// <summary>
        /// testing utility with Sqlite in-memory database
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// prepares service provider for each test
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
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
        /// creates new user and ensures user is input in database
        /// </summary>
        [Test]
        public void test_userExists_inDatabase(){
            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();

            var firstName = "Jane";
            var lastName = "Doe";
            var userName = "janeDoe_1234";
            var password = "aTc12n0L#";

            util._userBiz.createUser(userName, password, firstName, lastName);
            Assert.Less(0, util._userBiz.userExists("janeDoe_1234", "aTc12n0L#"));

            util._context.Database.CloseConnection();

        }

        /// <summary>
        /// creates new user
        /// checks whether unrelated user is in database
        /// </summary>
        [Test]
        public void test_userExists_notInDatabase(){
            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();

            var firstName = "Jane";
            var lastName = "Doe";
            var userName = "janeDoe_1234";
            var password = "aTc12n0L#";

            util._userBiz.createUser(userName, password, firstName, lastName);
            Assert.Less(0, util._userBiz.userExists("janeDoe_1234", "aTc12n0L#"));

            Assert.AreEqual(-1, util._userBiz.userExists("bobSmith_5678", "1a5b2C"));
            
            util._context.Database.CloseConnection();
        }

        /// <summary>
        /// creates new user
        /// inputs incorrect password
        /// </summary>
        [Test]
        public void test_userExists_wrongPassword(){
            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();

            var firstName = "Jane";
            var lastName = "Doe";
            var userName = "janeDoe_1234";
            var password = "aTc12n0L#";

            util._userBiz.createUser(userName, password, firstName, lastName);
            Assert.AreEqual(0, util._userBiz.userExists("janeDoe_1234", "8Gb2Adl#"));

            util._context.Database.CloseConnection();
        }

        /// <summary>
        /// create new user
        /// </summary>
        [Test]
        public void createUser_verify(){

            var serviceProvider = SetUp();
            var util = testingUtility(serviceProvider);

            util._context.Database.OpenConnection();
            util._context.Database.EnsureCreated();

            var firstName = "David";
            var lastName = "Jones";
            var userName = "daveJones_2468";
            var password = "g%ba1MB";

           Assert.Less(0,util._userBiz.createUser(firstName, lastName, userName, password));
           util._context.Database.CloseConnection();
        }

    }
}