using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApp.EFLayer.Models
{
    public partial class Account
    {
        public Account()
        {
            CorporateAccounts = new HashSet<CorporateAccount>();
            CurrentAccounts = new HashSet<CurrentAccount>();
            SavingsAccounts = new HashSet<SavingsAccount>();
            Transactions = new HashSet<Transaction>();
        }

        public int AccountNumber { get; set; }
        public string CustomerId { get; set; }
        public double Balance { get; set; }
        public DateTime Doc { get; set; }
        public string Tin { get; set; }
        public string Ifsc { get; set; }
        public bool IsDeleted { get; set; }
        public string AccountType { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<CorporateAccount> CorporateAccounts { get; set; }
        public virtual ICollection<CurrentAccount> CurrentAccounts { get; set; }
        public virtual ICollection<SavingsAccount> SavingsAccounts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
