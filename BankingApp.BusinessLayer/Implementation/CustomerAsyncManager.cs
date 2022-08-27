using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using BankingApp.EFLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Implementation
{
    public class CustomerAsyncManager : ICustomerAsyncManager
    {
        private readonly ICustomerAsyncRepository customerAsyncRepository;

        public CustomerAsyncManager(ICustomerAsyncRepository customerAsyncRepository)
        {
            this.customerAsyncRepository = customerAsyncRepository;
        }

        public Task<Transaction> FundTransfer(Transaction transaction)
            => this.customerAsyncRepository.FundTransfer(transaction);

        public int GetAccountIdByNumber(string accountNumber)
            => this.customerAsyncRepository.GetAccountIdByNumber(accountNumber);


        public Task<IEnumerable<TransactionStatement>> GetCustomizedStatement(CustomizedStatement customizedStatement)
            => this.customerAsyncRepository.GetCustomizedStatement(customizedStatement);

        public Task<IEnumerable<TransactionStatement>> GetMiniStatement(int accountId)
            => this.customerAsyncRepository.GetMiniStatement(accountId);

        Task<double> ICustomerAsyncManager.GetBalance(int accoundId)
            => this.customerAsyncRepository.GetBalance(accoundId);
    }
}
