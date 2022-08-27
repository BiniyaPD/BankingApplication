using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class AccountVM
    {
        [Display(Name = "Enter the Customer ID")]
        [Required(ErrorMessage = "Customer ID cannot be blank")]
        public string CustomerId { get; set; }

        [Display(Name = "Select the Account Type")]
        [Required(ErrorMessage = "Account Type cannot be blank")]
        public string AccountType { get; set; }

        [Display(Name = "Enter the Intial Balance")]
        [Required(ErrorMessage = "Intial Balance cannot be blank")]
        [RegularExpression(@"^[1-9]\d.*$", ErrorMessage = "Please enter a valid amount")]
        public float Balance { get; set; }

        [Display(Name = "Enter the PAN Number")]
        [Required(ErrorMessage = "Tin Number cannot be blank")]
        [RegularExpression("[A-Z]{5}[0-9]{4}[A-Z]{1}",ErrorMessage ="Please enter valid PAN Number")]
        public string Tin { get; set; }

        [Display(Name = "Enter the TIN Number(For Current Accounts Only")]
        //[Required(ErrorMessage = "Tin Number cannot be blank")]
        [RegularExpression("[0-9]{11}", ErrorMessage = "Please enter valid PAN Number")]
        public string TinNumber { get; set; }

    }
}
