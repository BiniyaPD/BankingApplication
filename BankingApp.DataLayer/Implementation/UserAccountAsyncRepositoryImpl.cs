using BankingApp.EFLayer.Contracts;
using BankingApp.EFLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EFLayer.Implementation
{
    public class UserAccountAsyncRepositoryImpl : IUserAccountAsyncRepository
    {
        private readonly db_bankContext context;

        public UserAccountAsyncRepositoryImpl(db_bankContext context)
        {
            this.context = context;
        }
        public async Task<bool> ValidateUser(string customerId, string password)
        {
            bool isValidated = false;
            var customerLogin = await this.context.Customerlogins.FirstOrDefaultAsync(x => x.CustomerId == customerId);
            if(customerLogin.CustomerId==customerId && customerLogin.Customerpassword==password)
            {
                isValidated = true;
            }
            return isValidated;
        }
    }
}
