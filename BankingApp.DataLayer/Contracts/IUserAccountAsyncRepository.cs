using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EFLayer.Contracts
{
    public interface IUserAccountAsyncRepository
    {
        Task<bool> ValidateUser(string customerId, string password);
    }
}
