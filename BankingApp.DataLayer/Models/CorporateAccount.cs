using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApp.EFLayer.Models
{
    public partial class CorporateAccount
    {
        public string CorporateAccountNo { get; set; }
        public int? AccountNumber { get; set; }

        public virtual Account AccountNumberNavigation { get; set; }
    }
}
