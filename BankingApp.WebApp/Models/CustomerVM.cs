using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Models
{
    public class CustomerVM
    {
        [Display(Name = "Enter the First Name")]
        [Required(ErrorMessage = "First Name cannot be blank")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please Enter Valid First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Enter the Last Name")]
        [Required(ErrorMessage = "Last Name cannot be blank")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please Enter Valid Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Select the Gender")]
        [Required(ErrorMessage = "Gender cannot be blank")]
        public string Gender { get; set; }

        [Display(Name = "Select the Date of Birth")]
        [Required(ErrorMessage = "Date of Birth cannot be blank")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Display(Name = "Enter the Email Id")]
        [Required(ErrorMessage = "Email Id cannot be blank")]
        [EmailAddress]
        public string EmailId { get; set; }

        [Display(Name = "Enter the Mobile Number")]
        [Required(ErrorMessage = "Mobile Number cannot be blank")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Please Enter valid phone number")]
        //[RegularExpression(@"^[0 - 9]*$", ErrorMessage = "Please Enter Valid Phone Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Enter the City Name")]
        [Required(ErrorMessage = "City Name cannot be blank")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please Enter Valid City Name")]
        public string City { get; set; }


        [Display(Name = "Enter the State Name")]
        [Required(ErrorMessage = "State Name cannot be blank")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Please Enter Valid State Name")]
        public string State { get; set; }

        [Display(Name = "Enter the Pin Code")]
        [Required(ErrorMessage = "Pin Code cannot be blank")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="Please enter valid pin number")]
        [MaxLength(6,ErrorMessage ="Please Enter valid Pin code")]
        [MinLength(6,ErrorMessage = "Please Enter valid Pin code")]
        public string Pincode { get; set; }
    }
}
