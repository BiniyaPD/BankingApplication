using BankingApp.EFLayer.Contracts;
using BankingApp.EFLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EFLayer.Implementation
{
    public class AccountRepositoryEFImpl:IAccountRepository
    {
        private readonly db_bankContext context;

        public AccountRepositoryEFImpl(db_bankContext context)
        {
            this.context = context;
        }

        public string AddCorporateAccount(CommonLayer.Models.CorporateAccount account)
        {
            string accountNumber = string.Empty;
            try
            {
                var duplicateAccount = this.context.Accounts.FirstOrDefault(x => x.CustomerId == account.Account.CustomerId
                                                                              && x.AccountType == "Corporate"
                                                                              && x.IsDeleted==false);
                if(duplicateAccount==null)
                {
                    var accountDb = new Models.Account()
                    {
                        CustomerId = account.Account.CustomerId,
                        Balance = account.Account.Balance,
                        Doc = account.Account.Doc,
                        Tin = account.Account.Tin,
                        Ifsc = account.Account.Ifsc,
                        AccountType = account.Account.AccountType,
                        IsDeleted = false
                    };
                    this.context.Add(accountDb);
                    this.context.SaveChanges();
                    int accountId = accountDb.AccountNumber;
                    var corporateAccountDb = new Models.CorporateAccount()
                    {
                        
                        AccountNumber = accountId
                    };
                    this.context.Add(corporateAccountDb);
                    this.context.SaveChanges();
                    accountNumber = corporateAccountDb.CorporateAccountNo;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return accountNumber;
        }

        public string AddCurrentAccount(CommonLayer.Models.CurrentAccount account)
        {
            string accountNumber = string.Empty;
            try
            {
                var duplicateAccount = this.context.Accounts.FirstOrDefault(x => x.CustomerId == account.Account.CustomerId
                                                                             && x.AccountType == "Current"
                                                                             && x.IsDeleted == false);
                if (duplicateAccount == null)
                {
                    var accountDb = new Models.Account()
                    {
                        CustomerId = account.Account.CustomerId,
                        Balance = account.Account.Balance,
                        Doc = account.Account.Doc,
                        Tin = account.Account.Tin,
                        Ifsc = account.Account.Ifsc,
                        AccountType = account.Account.AccountType,
                        IsDeleted = false
                    };
                    this.context.Add(accountDb);
                    this.context.SaveChanges();
                    int accountId = accountDb.AccountNumber;
                    var currentAccountDb = new Models.CurrentAccount()
                    {
                        TinNumber=account.TinNumber,
                        AccountNumber = accountId
                    };
                    this.context.Add(currentAccountDb);
                    this.context.SaveChanges();
                    accountNumber = currentAccountDb.CurrentAccountNo;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return accountNumber;
        }

        public string AddSavingsAccount(CommonLayer.Models.SavingsAccount account)
        {
            string accountNumber = string.Empty;
            try
            {
                var duplicateAccount = this.context.Accounts.FirstOrDefault(x => x.CustomerId == account.Account.CustomerId
                                                                             && x.AccountType == "Savings"
                                                                             && x.IsDeleted == false);
                if (duplicateAccount == null)
                {
                    var accountDb = new Models.Account()
                    {
                        CustomerId = account.Account.CustomerId,
                        Balance = account.Account.Balance,
                        Doc = account.Account.Doc,
                        Tin = account.Account.Tin,
                        Ifsc = account.Account.Ifsc,
                        AccountType = account.Account.AccountType,
                        IsDeleted = false
                    };
                    this.context.Add(accountDb);
                    this.context.SaveChanges();
                    int accountId = accountDb.AccountNumber;
                    var savingsAccountDb = new Models.SavingsAccount()
                    {
                       
                        AccountNumber = accountId
                    };
                    this.context.Add(savingsAccountDb);
                    this.context.SaveChanges();
                    accountNumber = savingsAccountDb.SavingsAccountNo;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return accountNumber;
        }

        public bool DeleteAccount(string accountNo)
        {
            bool isDeleted = false;
            try
            {
                int accountId = GetAccountIdByNumber(accountNo);
                var accountToDelete = this.context.Accounts.FirstOrDefault(x => x.AccountNumber == accountId);
                if (accountToDelete != null)
                {
                    accountToDelete.IsDeleted = true;
                    this.context.SaveChanges();
                    isDeleted = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return isDeleted;
        }



        public bool EditAccount(CommonLayer.Models.Account account)
        {
            bool isUpdated = false;
            try
            {
                var accountDb = this.context.Accounts.FirstOrDefault(x => x.AccountNumber == account.AccountNumber);
                string oldaccountType = accountDb.AccountType;
                if (account.AccountType == "Savings")
                {
                    accountDb.AccountType = account.AccountType;
                    accountDb.Tin = account.Tin;
                    accountDb.Balance = accountDb.Balance + account.Balance;
                    this.context.SaveChanges();
                    var savingsAccountDb = new Models.SavingsAccount()
                    {
                       
                        AccountNumber = account.AccountNumber
                    };
                    this.context.SavingsAccounts.Add(savingsAccountDb);
                    this.context.SaveChanges();
                    isUpdated = true;
                }
                else if (account.AccountType == "Current")
                {
                    accountDb.AccountType = account.AccountType;
                    accountDb.Tin = account.Tin;
                    accountDb.Balance = accountDb.Balance + account.Balance;
                    this.context.SaveChanges();
                    var currentAccountDb = new Models.CurrentAccount()
                    {
                        TinNumber=account.TinNumber,
                        AccountNumber = account.AccountNumber
                    };
                    this.context.CurrentAccounts.Add(currentAccountDb);
                    this.context.SaveChanges();
                    isUpdated = true;
                }
                else
                {
                    accountDb.AccountType = account.AccountType;
                    accountDb.Tin = account.Tin;
                    accountDb.Balance = accountDb.Balance + account.Balance;
                    this.context.SaveChanges();
                    var corporateAccountDb = new Models.CorporateAccount()
                    { 
                        AccountNumber = account.AccountNumber
                    };
                    this.context.CorporateAccounts.Add(corporateAccountDb);
                    this.context.SaveChanges();
                    isUpdated = true;
                }
                if (oldaccountType == "Savings")
                {
                    var savings = this.context.SavingsAccounts.FirstOrDefault(x => x.AccountNumber == accountDb.AccountNumber);
                    this.context.SavingsAccounts.Remove(savings);
                    this.context.SaveChanges();
                }
                else if (oldaccountType == "Corporate")
                {
                    var corporate = this.context.CorporateAccounts.FirstOrDefault(x => x.AccountNumber == accountDb.AccountNumber);
                    this.context.CorporateAccounts.Remove(corporate);
                    this.context.SaveChanges();
                }
                else
                {
                    var current = this.context.CurrentAccounts.FirstOrDefault(x => x.AccountNumber == accountDb.AccountNumber);
                    this.context.CurrentAccounts.Remove(current);
                    this.context.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return isUpdated;
        }


        public int GetAccountIdByNumber(string accountNumber)
        {
            int accountId=0;
            if(accountNumber.Contains("SA"))
            {
                var savings = this.context.SavingsAccounts.FirstOrDefault(x => x.SavingsAccountNo == accountNumber && x.AccountNumberNavigation.IsDeleted==false);
                if(savings!=null)
                {
                    accountId = savings.AccountNumber.Value;
                }
            }
            else if(accountNumber.Contains("CA"))
            {
                var current = this.context.CurrentAccounts.FirstOrDefault(x => x.CurrentAccountNo == accountNumber && x.AccountNumberNavigation.IsDeleted == false);
                if(current!=null)
                {
                    accountId = current.AccountNumber.Value;
                }
            }
            else
            {
                var corporate = this.context.CorporateAccounts.FirstOrDefault(x => x.CorporateAccountNo == accountNumber && x.AccountNumberNavigation.IsDeleted == false);
                if(corporate!=null)
                {
                    accountId = corporate.AccountNumber.Value;
                }
            }
            return accountId;
        }

        public IEnumerable<CommonLayer.Models.Account> GetAccountByCustomerId(string customerId)
        {
            var accountDb = this.context.Accounts.ToList();
            var accounts = from a in accountDb
                           where a.CustomerId == customerId && a.IsDeleted == false
                           select new CommonLayer.Models.Account()
                           {
                               CustomerId = a.CustomerId,
                               Balance = a.Balance,
                               Doc = a.Doc,
                               Tin = a.Tin,
                               Ifsc = a.Ifsc,
                               AccountType = a.AccountType

                           };
            return accounts.ToList();

        }

        public CommonLayer.Models.Account GetAccountById(int id)
        {
            CommonLayer.Models.Account account = null;
            try
            {
                var accountDb = this.context.Accounts.FirstOrDefault(x => x.AccountNumber == id && x.IsDeleted == false);
                if (accountDb != null)
                {
                    account = new CommonLayer.Models.Account()
                    {
                        CustomerId = accountDb.CustomerId,
                        AccountType = accountDb.AccountType,
                        Balance = accountDb.Balance,
                        Tin = accountDb.Tin
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return account;
        }
    }
}
