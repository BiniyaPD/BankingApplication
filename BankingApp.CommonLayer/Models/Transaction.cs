using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.CommonLayer.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int? SourceAccountNo { get; set; }
        public double TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? DestinationAccountNo { get; set; }
        public string SourseAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public string TransactionDescription { get; set; }

        public Account SourceAccount { get; set; }
    }
}
