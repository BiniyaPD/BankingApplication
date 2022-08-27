using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using BankingApp.EFLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankingApp.BusinessLayer.Implementation
{
    public class TransactionManager : ITransactionManager
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionManager(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public double DailyTransactionsAmount(int accountId)
            => this.transactionRepository.DailyTransactionsAmount(accountId);

        public bool Deposit(CommonLayer.Models.Transaction transaction)
            => this.transactionRepository.Deposit(transaction);

        public IEnumerable<CommonLayer.Models.Transaction> GetCustomizedStatement(CustomizedStatement customizedStatement)
            => this.transactionRepository.GetCustomizedStatement(customizedStatement);
        

        public IEnumerable<CommonLayer.Models.Transaction> GetMiniStatement(int accountId)
            => this.transactionRepository.GetMiniStatement(accountId);

        public bool Transfer(CommonLayer.Models.Transaction transaction)
            => this.transactionRepository.Transfer(transaction);

        public bool WithDraw(CommonLayer.Models.Transaction transaction)
            => this.transactionRepository.WithDraw(transaction);
    }
}
