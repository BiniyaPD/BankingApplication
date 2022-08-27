using System;
using System.Collections.Generic;

#nullable disable

namespace BankingApp.EFLayer.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Accounts = new HashSet<Account>();
        }

        public string CustomerId { get; set; }
        public string ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
