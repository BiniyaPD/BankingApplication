using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class ChangePassword
    {
        [Display(Name = "Enter Old Password")]
        [Required(ErrorMessage = "Password cannot be blank")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "Enter New Password")]
        [Required(ErrorMessage = "Password cannot be blank")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Enter Confirm Password")]
        [Required(ErrorMessage = "Confirm Password cannot be blank")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password does not match with password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
