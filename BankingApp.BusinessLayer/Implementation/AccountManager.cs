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
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository accountRepository;

        public AccountManager(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public string AddCorporateAccount(CorporateAccount account)
            => this.accountRepository.AddCorporateAccount(account);

        public string AddCurrentAccount(CurrentAccount account)
            => this.accountRepository.AddCurrentAccount(account);

        public string AddSavingsAccount(SavingsAccount account)
            => this.accountRepository.AddSavingsAccount(account);

        public bool DeleteAccount(string accountNo)
            => this.accountRepository.DeleteAccount(accountNo);

        public bool EditAccount(Account account)
            => this.accountRepository.EditAccount(account);

        public IEnumerable<Account> GetAccountByCustomerId(string customerId)
            => this.accountRepository.GetAccountByCustomerId(customerId);

        public Account GetAccountById(int id)
            => this.accountRepository.GetAccountById(id);

        public int GetAccountIdByNumber(string accountNumber)
            => this.accountRepository.GetAccountIdByNumber(accountNumber);

       
    }
}
