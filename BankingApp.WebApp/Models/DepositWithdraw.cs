using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class DepositWithdraw
    {
        [Display(Name = "Enter the Account Number")]
        [Required(ErrorMessage = "Account number cannot be blank")]
        public string AccountNumber { get; set; }

        [Display(Name = "Enter the Amount")]
        [Required(ErrorMessage = "Amount cannot be blank")]
        [RegularExpression(@"^[1-9]\d.*$",ErrorMessage ="Please enter a valid amount")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        [Display(Name = "Enter the Description")]
        [Required(ErrorMessage = "Description cannot be blank")]
        public string Description { get; set; }

    }
}
