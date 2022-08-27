using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.CommonLayer.Models
{
    public class Manager
    {
        public string ManagerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string ManagerPassword { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }

        private List<Customer> customers = new List<Customer>();
        public void AddCustomer(Customer customer)
        {
            this.customers.Add(customer);
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return this.customers;
        }

    }
}
