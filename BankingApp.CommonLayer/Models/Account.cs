using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.CommonLayer.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public string CustomerId { get; set; }
        public double Balance { get; set; }
        public DateTime Doc { get; set; }
        public string Tin { get; set; }
        public string TinNumber { get; set; }
        public string Ifsc { get; set; }
        public bool IsDeleted { get; set; }
        public string AccountType { get; set; }
        public Customer Customer { get; set; }

        private List<SavingsAccount> savingsAccounts = new List<SavingsAccount>();

        private List<CurrentAccount> currentAccounts = new List<CurrentAccount>();

        private List<CorporateAccount> corporateAccounts = new List<CorporateAccount>();

        private List<Transaction> transactions = new List<Transaction>();

        
        public IEnumerable<SavingsAccount> GetSavingsAccounts()
        {
            return this.savingsAccounts;
        }
        
        public IEnumerable<CurrentAccount> GetCurrentAccounts()
        {
            return this.currentAccounts;
        }
        
        public IEnumerable<CorporateAccount> GetCorporateAccounts()
        {
            return this.corporateAccounts;
        }
        
        public IEnumerable<Transaction> GetTransactions()
        {
            return this.transactions;
        }


    }
}
