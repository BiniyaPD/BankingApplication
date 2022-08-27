using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class ManagerLogin
    {
        [Display(Name = "Enter your Manager Id")]
        [Required(ErrorMessage = "Manager ID cannot be blank")]
        public string LoginId { get; set; }

        [Display(Name = "Enter your Password")]
        [Required(ErrorMessage = "Password cannot be blank")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
