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
    public class UserController : ControllerBase
    {

        public class UserBasics{
            public string firstName;
            public string lastName;
            public string userName;
            public string password;

        }
        IUserBiz _userBiz;
        public UserController(IUserBiz userBiz){
            _userBiz = userBiz;
        }

        [HttpPost("login")]
        public ActionResult<int> Login(UserBasics logInUser)
        {   
            return _userBiz.userExists(logInUser.userName, logInUser.password);
            
        }

        // // POST api/user/register
        //changed return type from string to int...may cause problems?
        [HttpPost("register")]
        public ActionResult<int> Register([FromBody] UserBasics UserRegisterUser)
        {
            return _userBiz.createUser(UserRegisterUser.userName, 
                                       UserRegisterUser.password, 
                                       UserRegisterUser.firstName, 
                                       UserRegisterUser.lastName);
        }
    }
}
