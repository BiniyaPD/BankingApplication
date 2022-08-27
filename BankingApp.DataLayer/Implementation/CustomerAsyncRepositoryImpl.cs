using BankingApp.CommonLayer.Models;
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
    public class CustomerAsyncRepositoryImpl : ICustomerAsyncRepository
    {
        private readonly db_bankContext context;

        public CustomerAsyncRepositoryImpl(db_bankContext context)
        {
            this.context = context;
        }
        


        private string GetAccountNumberById(int? sourceAccountNo)
        {
            string accountNumber = string.Empty;
            var savings = this.context.SavingsAccounts.FirstOrDefault(x => x.AccountNumber == sourceAccountNo);
            var current = this.context.CurrentAccounts.FirstOrDefault(x => x.AccountNumber == sourceAccountNo);
            var corporate = this.context.CorporateAccounts.FirstOrDefault(x => x.AccountNumber == sourceAccountNo);
            if (savings != null)
            {
                accountNumber = savings.SavingsAccountNo;
            }
            if (current != null)
            {
                accountNumber = current.CurrentAccountNo;
            }
            if (corporate != null)
            {
                accountNumber = corporate.CorporateAccountNo;
            }

            return accountNumber;
        }

        public int GetAccountIdByNumber(string accountNumber)
        {
            int accountId = 0;
            if (accountNumber.Contains("SA"))
            {
                var savings = this.context.SavingsAccounts.FirstOrDefault(x => x.SavingsAccountNo == accountNumber && x.AccountNumberNavigation.IsDeleted == false);
                if (savings != null)
                {
                    accountId = savings.AccountNumber.Value;
                }
            }
            else if (accountNumber.Contains("CA"))
            {
                var current = this.context.CurrentAccounts.FirstOrDefault(x => x.CurrentAccountNo == accountNumber && x.AccountNumberNavigation.IsDeleted == false);
                if (current != null)
                {
                    accountId = current.AccountNumber.Value;
                }
            }
            else
            {
                var corporate = this.context.CorporateAccounts.FirstOrDefault(x => x.CorporateAccountNo == accountNumber && x.AccountNumberNavigation.IsDeleted == false);
                if (corporate != null)
                {
                    accountId = corporate.AccountNumber.Value;
                }
            }
            return accountId;
        }

        public async Task<IEnumerable<CommonLayer.Models.TransactionStatement>> GetMiniStatement(int accountId)
        {
            IEnumerable<CommonLayer.Models.TransactionStatement> transactions = null;
            try
            {
                var transactionsDb = await this.context.Transactions.ToListAsync();
                transactions = (from t in transactionsDb
                                where t.SourceAccountNo == accountId
                                orderby t.TransactionId descending
                                select new CommonLayer.Models.TransactionStatement()
                                {
                                    TransactionId = t.TransactionId,
                                    SourseAccountNumber = GetAccountNumberById(t.SourceAccountNo),
                                    TransactionAmount = t.TransactionAmount,
                                    TransactionType = t.TransactionType,
                                    TransactionDate = t.TransactionDate,
                                    DestinationAccountNumber = GetAccountNumberById(t.DestinationAccountNo),
                                    TransactionDescription = t.TransactionDescription
                                }).Take(5).ToList();
                
            }
            catch (Exception)
            {

                throw;
            }
            return transactions.ToList();
        }

        public async Task<double> GetBalance(int accoundId)
        {
            double amount = 0;
            var account = await this.context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == accoundId && x.IsDeleted == false);
            if(account!=null)
            {
                amount = account.Balance;
            }
            return amount;
        }

        public async Task<CommonLayer.Models.Transaction> FundTransfer(CommonLayer.Models.Transaction transaction)
        {
            CommonLayer.Models.Transaction transactionReturn = null;
            try
            {
                var sAccountDb = await this.context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == transaction.SourceAccountNo);
                var dAccountDb = await this.context.Accounts.FirstOrDefaultAsync(x => x.AccountNumber == transaction.DestinationAccountNo);
                if (sAccountDb != null && dAccountDb != null)
                {
                    sAccountDb.Balance -= transaction.TransactionAmount;
                    dAccountDb.Balance += transaction.TransactionAmount;
                    await this.context.SaveChangesAsync();
                    var transactionDb = new Models.Transaction()
                    {
                        SourceAccountNo = transaction.SourceAccountNo,
                        TransactionAmount = transaction.TransactionAmount,
                        TransactionType = "Transfer",
                        TransactionDate = DateTime.UtcNow,
                        DestinationAccountNo = transaction.DestinationAccountNo,
                        TransactionDescription = transaction.TransactionDescription
                    };
                    var result=await this.context.Transactions.AddAsync(transactionDb);
                    await this.context.SaveChangesAsync();
                    transactionReturn = new CommonLayer.Models.Transaction()
                    {
                        SourseAccountNumber= GetAccountNumberById(result.Entity.SourceAccountNo),
                        TransactionAmount =result.Entity.TransactionAmount,
                        DestinationAccountNumber= GetAccountNumberById(result.Entity.DestinationAccountNo)
                    };
                    
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return transactionReturn;
        }

        public async Task<IEnumerable<CommonLayer.Models.TransactionStatement>> GetCustomizedStatement(CustomizedStatement customizedStatement)
        {
            IEnumerable<CommonLayer.Models.TransactionStatement> transactionStatement = null;
            try
            {
                var transactionsDb = await this.context.Transactions.ToListAsync();
                transactionStatement = (from t in transactionsDb
                                where t.SourceAccountNo == customizedStatement.AccountId &&
                                (t.TransactionAmount >= customizedStatement.LowerLimit) &&
                                (t.TransactionDate >= customizedStatement.FromDate && t.TransactionDate <= customizedStatement.ToDate)
                                orderby t.TransactionId descending
                                select new CommonLayer.Models.TransactionStatement()
                                {
                                    TransactionId = t.TransactionId,
                                    SourseAccountNumber = GetAccountNumberById(t.SourceAccountNo),
                                    TransactionAmount = t.TransactionAmount,
                                    TransactionType = t.TransactionType,
                                    TransactionDate = t.TransactionDate,
                                    DestinationAccountNumber = GetAccountNumberById(t.DestinationAccountNo),
                                    TransactionDescription = t.TransactionDescription
                                }).Take(customizedStatement.NoOfTransaction);
               
            }
            catch (Exception)
            {

                throw;
            }
            return transactionStatement;
        }
    
    }
}
