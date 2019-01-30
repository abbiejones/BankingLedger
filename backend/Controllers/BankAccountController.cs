using System;
using Microsoft.AspNetCore.Mvc;
using BankingLedger.Biz;
using System.Collections.Generic;

namespace BankingLedgerApi.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        public class AccountInfo{

            public int userId;
            public int accountNumber;
            public decimal amount;
        }

        IBankAccountBiz _bankAccountBiz;
        IUserBiz _userBiz;
        public BankAccountController(IBankAccountBiz bankAccountBiz, IUserBiz userBiz){
            _bankAccountBiz = bankAccountBiz;
            _userBiz = userBiz;
        }

        [HttpPost("deposit")]
        public ActionResult<Tuple<int, decimal>> Deposit([FromBody] AccountInfo depositInfo)
        {   
            _userBiz.setUser(depositInfo.userId);
            return _bankAccountBiz.deposit(depositInfo.amount, depositInfo.accountNumber);
        }

        // // POST api/user/register
        [HttpPost("withdraw")] 
        public ActionResult<Tuple<int, decimal>> Withdraw([FromBody] AccountInfo withdrawInfo)
        {
            _userBiz.setUser(withdrawInfo.userId);
            return _bankAccountBiz.withdraw(withdrawInfo.amount, withdrawInfo.accountNumber);
        }

        //TODO: set up web transactions
        [HttpGet("transaction/{userId}")]
        public ActionResult<List<List<string>>> Transaction(int userId)
        {
            _userBiz.setUser(userId);
            return _bankAccountBiz.getTransactionHistory();
        }

        [HttpGet("balance/{userCheck}/{userId}")]
        public ActionResult<Tuple<int, decimal>> Balance(bool userCheck, int userId)
        {   _userBiz.setUser(userId);
            return _bankAccountBiz.checkBalance(userCheck);
        }
        
    }
}