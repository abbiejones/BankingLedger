using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using BankingLedger.Biz;

namespace BankingLedgerApi.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        public class AccountInfo{
            public int accountNumber;
            public decimal amount;
        }

        IBankAccountBiz _bankAccountBiz;
        public BankAccountController(IBankAccountBiz bankAccountBiz){
            _bankAccountBiz = bankAccountBiz;
        }

        [HttpPost("deposit")]
        public ActionResult<Tuple<int, decimal>> Deposit([FromBody] AccountInfo depositInfo)
        {   
            return _bankAccountBiz.deposit(depositInfo.amount, depositInfo.accountNumber);
        }

        // // POST api/user/register
        [HttpPost("withdraw")] 
        public ActionResult<Tuple<int, decimal>> Withdraw([FromBody] AccountInfo withdrawInfo)
        {
            return _bankAccountBiz.withdraw(withdrawInfo.amount, withdrawInfo.accountNumber);
        }

        //TODO: set up web transactions
        [HttpGet("transaction")]
        public ActionResult<string> Transaction()
        {
            //get all transactions
            return "1";
        }

        [HttpGet("balance/{userCheck}")]
        public ActionResult<Tuple<int, decimal>> Balance(bool userCheck)
        {
            return _bankAccountBiz.checkBalance();
        }
        
    }
}