using BankingLedger.DataAccess;
using Microsoft.EntityFrameworkCore;
using BankingLedger.Biz;
using BankingLedger.Manager;
using Microsoft.Extensions.DependencyInjection;
using BankingLedger.Log;

namespace BankingLedger
{
    class Program
    {

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
        static void Main(string[] args)
        {
            var serviceProvider = configureServices();

            var bank = new BankingInterface(serviceProvider.GetService<IUserBiz>(), serviceProvider.GetService<IBankAccountBiz>());
            bank.application();

        }
    }
}
