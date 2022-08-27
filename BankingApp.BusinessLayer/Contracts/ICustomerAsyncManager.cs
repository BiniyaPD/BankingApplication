using BankingApp.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Contracts
{
    public interface ICustomerAsyncManager
    {
        /// <summary>
        /// Method to Get the Balance
        /// </summary>
        /// <param name="accoundId"></param>
        /// <returns></returns>
        Task<double> GetBalance(int accoundId);
        /// <summary>
        /// Method to get account Id by account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        int GetAccountIdByNumber(string accountNumber);
        /// <summary>
        /// Method to get the last 5 transaction
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IEnumerable<TransactionStatement>> GetMiniStatement(int accountId);
        /// <summary>
        /// Method to get the customized transaction
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IEnumerable<TransactionStatement>> GetCustomizedStatement(CustomizedStatement customizedStatement);
        /// <summary>
        /// Method to Transfer amount from one account to another
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<Transaction> FundTransfer(Transaction transaction);
    }
}

