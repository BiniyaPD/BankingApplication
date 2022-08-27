using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using BankingApp.EFLayer.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountAsyncManager userAccountAsyncManager;
        private readonly ICustomerManager customerManager;

        public UserAccountController(IUserAccountAsyncManager userAccountAsyncManager, ICustomerManager customerManager)
        {
            this.userAccountAsyncManager = userAccountAsyncManager;
            this.customerManager = customerManager;
        }

        [HttpPost]
        [Route("GetUserToken")]
        public async Task<TokenResponse> GetUserToken(string customerId,string password)
        {
       
            return await userAccountAsyncManager.GetUserToken(customerId,password);
            
        }
    }
}
