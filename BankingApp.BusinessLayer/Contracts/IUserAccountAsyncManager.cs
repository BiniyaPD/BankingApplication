using BankingApp.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Contracts
{
    public interface IUserAccountAsyncManager
    {
        Task<TokenResponse> GetUserToken(string customerId,string password);
    }
}
