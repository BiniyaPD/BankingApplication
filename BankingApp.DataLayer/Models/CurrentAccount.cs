using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApp.EFLayer.Models
{
    public partial class CurrentAccount
    {
        public string CurrentAccountNo { get; set; }
        public int? AccountNumber { get; set; }
        public string TinNumber { get; set; }

        public virtual Account AccountNumberNavigation { get; set; }
    }
}
