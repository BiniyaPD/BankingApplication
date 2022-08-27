using BankingApp.EFLayer.Contracts;
using BankingApp.EFLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankingApp.EFLayer.Implementation
{
    public class TransactionRepositoryEFImpl : ITransactionRepository
    {
        private readonly db_bankContext context;

        public TransactionRepositoryEFImpl(db_bankContext context)
        {
            this.context = context;
        }

        public double DailyTransactionsAmount(int accountId)
        {
            double total = 0;
            //IEnumerable<CommonLayer.Models.Transaction> transactions = null;
            try
            {
                var transactionsDb = this.context.Transactions.ToList();
                var transactions = from t in transactionsDb
                               where t.SourceAccountNo == accountId &&
                               t.TransactionDate == DateTime.UtcNow &&
                               (t.TransactionType == "Withdraw" || t.TransactionType == "Transfer")
                               select new CommonLayer.Models.Transaction()
                               {
                                   TransactionAmount = t.TransactionAmount,
                                   TransactionDate = t.TransactionDate,
                                   TransactionType = t.TransactionType
                               };
                foreach (var transaction in transactions)
                {
                    total += transaction.TransactionAmount;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return total;

        }

        public bool Deposit(CommonLayer.Models.Transaction transaction)
        {
            bool isDeposited = false;
            try
            {
                var accountDb = this.context.Accounts.FirstOrDefault(x => x.AccountNumber == transaction.SourceAccountNo);
                if (accountDb != null)
                {
                    accountDb.Balance += transaction.TransactionAmount;
                    this.context.SaveChanges();
                    var transactionDb = new Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionType = transaction.TransactionType,
                        TransactionDate = DateTime.UtcNow,
                        DestinationAccountNo = null,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    this.context.Transactions.Add(transactionDb);
                    this.context.SaveChanges();
                    isDeposited = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return isDeposited;
        }

        public IEnumerable<CommonLayer.Models.Transaction> GetMiniStatement(int accountId)
        {
            IEnumerable<CommonLayer.Models.Transaction> transactions = null;
            try
            {
                var transactionsDb = this.context.Transactions.ToList();
                transactions = (from t in transactionsDb
                               where t.SourceAccountNo == accountId
                               orderby t.TransactionId descending
                               select new CommonLayer.Models.Transaction()
                               {
                                   TransactionId=t.TransactionId,
                                   SourceAccountNo = t.SourceAccountNo,
                                   SourseAccountNumber = GetAccountNumberById(t.SourceAccountNo),
                                   TransactionAmount = t.TransactionAmount,
                                   TransactionType = t.TransactionType,
                                   TransactionDate = t.TransactionDate,
                                   DestinationAccountNo = t.DestinationAccountNo,
                                   DestinationAccountNumber = GetAccountNumberById(t.DestinationAccountNo),
                                   TransactionDescription = t.TransactionDescription
                               }).Take(5);
            }
            catch (Exception)
            {

                throw;
            }
            return transactions.ToList();
        }

        private string GetAccountNumberById(int? sourceAccountNo)
        {
            string accountNumber = string.Empty;
            var savings = this.context.SavingsAccounts.FirstOrDefault(x => x.AccountNumber == sourceAccountNo);
            var current = this.context.CurrentAccounts.FirstOrDefault(x => x.AccountNumber == sourceAccountNo);
            var corporate = this.context.CorporateAccounts.FirstOrDefault(x => x.AccountNumber == sourceAccountNo);
            if(savings!=null)
            {
                accountNumber = savings.SavingsAccountNo;
            }
            if(current!=null)
            {
                accountNumber = current.CurrentAccountNo;
            }
            if(corporate!=null)
            {
                accountNumber = corporate.CorporateAccountNo;
            }
            
            return accountNumber;
        }

        public bool Transfer(CommonLayer.Models.Transaction transaction)
        {
            bool isTransfered = false;
            try
            {
                var sAccountDb = this.context.Accounts.FirstOrDefault(x => x.AccountNumber == transaction.SourceAccountNo);
                var dAccountDb = this.context.Accounts.FirstOrDefault(x => x.AccountNumber == transaction.DestinationAccountNo);
                if (sAccountDb != null && dAccountDb!=null)
                {
                    sAccountDb.Balance -= transaction.TransactionAmount;
                    dAccountDb.Balance += transaction.TransactionAmount;
                    this.context.SaveChanges();
                    var transactionDb = new Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionType = transaction.TransactionType,
                        TransactionDate = DateTime.UtcNow,
                        DestinationAccountNo = transaction.DestinationAccountNo,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    this.context.Transactions.Add(transactionDb);
                    this.context.SaveChanges();
                    isTransfered = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return isTransfered;
        }

        public bool WithDraw(CommonLayer.Models.Transaction transaction)
        {
            bool isWithdraw = false;
            try
            {
                var accountDb = this.context.Accounts.FirstOrDefault(x => x.AccountNumber == transaction.SourceAccountNo);
                if (accountDb != null)
                {
                    accountDb.Balance -= transaction.TransactionAmount;
                    this.context.SaveChanges();
                    var transactionDb = new Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionType = transaction.TransactionType,
                        TransactionDate = DateTime.Now,
                        DestinationAccountNo = null,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    this.context.Transactions.Add(transactionDb);
                    this.context.SaveChanges();
                    isWithdraw = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return isWithdraw;
        }

        public IEnumerable<CommonLayer.Models.Transaction> GetCustomizedStatement(CommonLayer.Models.CustomizedStatement statement)
        {
            IEnumerable<CommonLayer.Models.Transaction> transactions = null;
            try
            {
                var transactionsDb = this.context.Transactions.ToList();
                transactions = (from t in transactionsDb
                                where t.SourceAccountNo == statement.AccountId &&
                                (t.TransactionAmount >= statement.LowerLimit) &&
                                (t.TransactionDate>=statement.FromDate && t.TransactionDate<=statement.ToDate)
                                orderby t.TransactionId descending
                                select new CommonLayer.Models.Transaction()
                                {
                                    TransactionId = t.TransactionId,
                                    SourceAccountNo = t.SourceAccountNo,
                                    SourseAccountNumber = GetAccountNumberById(t.SourceAccountNo),
                                    TransactionAmount = t.TransactionAmount,
                                    TransactionType = t.TransactionType,
                                    TransactionDate = t.TransactionDate,
                                    DestinationAccountNo = t.DestinationAccountNo,
                                    DestinationAccountNumber = GetAccountNumberById(t.DestinationAccountNo),
                                    TransactionDescription = t.TransactionDescription
                                }).Take(statement.NoOfTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            return transactions.ToList();
        }
    }
}
