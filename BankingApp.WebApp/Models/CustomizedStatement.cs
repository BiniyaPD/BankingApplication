using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class CustomizedStatement
    {
        [Display(Name = "Enter the Account Number")]
        [Required(ErrorMessage = "Account number cannot be blank")]
        public string AccountNumber { get; set; }

        [Display(Name = "Select the From Date")]
        [Required(ErrorMessage = "From date cannot be blank")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Select the To Date")]
        [Required(ErrorMessage = "To date cannot be blank")]
        public DateTime ToDate { get; set; }

        [Display(Name = "Enter the Lower Limit")]
        [Required(ErrorMessage = "Lower limit cannot be blank")]
        [RegularExpression(@"^[1-9]\d.*$", ErrorMessage = "Please enter a valid lower limit amount")]
        public float LowerLimit { get; set; }

        [Display(Name = "Enter the Number Of Transactions")]
        [Required(ErrorMessage = "Number of Transactions cannot be blank")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter valid number of transactions")]
        public int NoOfTransaction { get; set; }
    }
}
