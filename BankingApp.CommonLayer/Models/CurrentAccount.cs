﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.CommonLayer.Models
{
    public class CurrentAccount
    {
        //public string CurrentAccountNo { get; set; }
        public int AccountNumber { get; set; }
        public float MinimumBalance { get; set; }
        public string TinNumber { get; set; }
        public  Account Account { get; set; }
    }
}
