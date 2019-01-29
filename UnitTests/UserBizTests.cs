using NUnit.Framework;
using BankingLedger.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BankingLedger.Log;
using BankingLedger.Biz;

namespace BankingLedger.UnitTests
{

    [TestFixture]
    public class UserBizTests
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