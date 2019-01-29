using BankingLedger.DataAccess;
using Microsoft.EntityFrameworkCore;
using BankingLedger.Biz;
using BankingLedger.Manager;
using Microsoft.Extensions.DependencyInjection;
using BankingLedger.Log;

/// <summary>
/// app entry point
/// </summary>
namespace BankingLedger
{
    class Program
    {
        /// <summary>
        /// generates service provider to allow for dependency injection into banking ledger application
        /// </summary>
        /// <returns>Service Provider</returns>
        public static ServiceProvider configureServices(){
            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlite()
            .AddSingleton<IBankAccountRepository, BankAccountRepository>()
            .AddSingleton<ITransactionRepository, TransactionRepository>()
            .AddSingleton<IUserRepository, UserRepository>()
            .AddSingleton<IUserBiz, UserBiz>()
            .AddSingleton<IBankAccountBiz, BankAccountBiz>()
            .AddSingleton<ILogger,Logger>()
            .AddOptions()
            .AddDbContext<BankingContext>(options => options.UseSqlite("DataSource=bank.db"))
            .BuildServiceProvider();

            return serviceProvider;
        }

        /// <summary>
        /// entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var serviceProvider = configureServices();

            var bank = new BankingInterface(serviceProvider.GetService<IUserBiz>(), serviceProvider.GetService<IBankAccountBiz>());
            bank.application();

        }
    }
}
