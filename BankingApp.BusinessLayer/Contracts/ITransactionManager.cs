using BankingApp.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Contracts
{
    public interface ITransactionManager
    {
        /// <summary>
        /// Method to Deposit Amount
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool Deposit(Transaction transaction);
        /// <summary>
        /// Method to Withdraw Amount
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool WithDraw(Transaction transaction);
        /// <summary>
        /// To Check whether the daily transaction limit exceed or not
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        double DailyTransactionsAmount(int accountId);
        /// <summary>
        /// Method to Transfer amount from one account to another
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        bool Transfer(Transaction transaction);
        /// <summary>
        /// Method to get the last 5 transaction
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        IEnumerable<Transaction> GetMiniStatement(int accountId);
        /// <summary>
        /// Method to Get Customized statement
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        IEnumerable<Transaction> GetCustomizedStatement(CustomizedStatement customizedStatement);
    }
}
