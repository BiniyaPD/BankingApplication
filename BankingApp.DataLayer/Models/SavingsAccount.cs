using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApp.EFLayer.Models
{
    public partial class SavingsAccount
    {
        public string SavingsAccountNo { get; set; }
        public int? AccountNumber { get; set; }

        public virtual Account AccountNumberNavigation { get; set; }
    }
}
