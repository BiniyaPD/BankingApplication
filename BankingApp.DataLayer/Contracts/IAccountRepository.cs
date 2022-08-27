using BankingApp.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EFLayer.Contracts
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Method to Add Savings Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        string AddSavingsAccount(SavingsAccount account);
        /// <summary>
        /// Method to Add Current Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        string AddCurrentAccount(CurrentAccount account);
        /// <summary>
        /// Method to Add Corporate Account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        string AddCorporateAccount(CorporateAccount account);
        /// <summary>
        /// Method to edit account details
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool EditAccount(Account account);
        /// <summary>
        /// Method to delete account
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        bool DeleteAccount(string accountNo);
        
        /// <summary>
        /// Method to get account Id by account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        int GetAccountIdByNumber(string accountNumber);
        /// <summary>
        /// Method to get all the account details of customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IEnumerable<Account> GetAccountByCustomerId(string customerId);
        /// <summary>
        /// Method to get the account details based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Account GetAccountById(int id);

    }
}
