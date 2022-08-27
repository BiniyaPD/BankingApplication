using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class CustomerIdSubmit
    {
        [Display(Name = "Enter the Customer ID")]
        [Required(ErrorMessage = "Customer ID cannot be blank")]
        public string CustomerId { get; set; }
    }
}
