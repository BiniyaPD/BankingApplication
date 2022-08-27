using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.CommonLayer.Models
{
    public class CustomizedStatement
    {
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public float LowerLimit { get; set; }
        public int NoOfTransaction { get; set; }
    }
}
