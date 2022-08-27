using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class AccountNumberSubmit
    {
        [Display(Name = "Enter the Account Number")]
        [Required(ErrorMessage = "Account Number cannot be blank")]
        public string AccountNumber { get; set; }
    }
}
