using BankingLedger.Biz;
using BankingLedger.DataAccess;

namespace BankingLedger.UnitTests
{
    public class TestingUtility
    {
        public IUserBiz _userBiz;

        public IBankAccountBiz _bankAccountBiz;
        public readonly BankingContext _context;
        public IUserRepository _userRepository;

        public IBankAccountRepository _bankAccountRepository;
        public ITransactionRepository _transactionRepository;

        public TestingUtility(IUserBiz userBiz,
                              IBankAccountBiz bankAccountBiz,
                              BankingContext context,
                              IUserRepository userRepository, 
                              IBankAccountRepository bankAccountRepository,
                              ITransactionRepository transactionRepository){
                                  
            _userBiz = userBiz;
            _bankAccountBiz = bankAccountBiz;
            _context = context;
            _userRepository = userRepository;
            _bankAccountRepository = bankAccountRepository;
            _transactionRepository = transactionRepository;

        }
    }
}