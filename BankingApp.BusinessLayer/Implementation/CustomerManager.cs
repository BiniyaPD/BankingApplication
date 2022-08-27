using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using BankingApp.EFLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.BusinessLayer.Implementation
{
    public class CustomerManager : ICustomerManager
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerManager(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        public string AddCustomer(Customer customer)
            => this.customerRepository.AddCustomer(customer);


        public bool DeleteCustomer(string customerId)
            => this.customerRepository.DeleteCustomer(customerId);

        public bool EditCustomer(Customer customer)
            => this.customerRepository.EditCustomer(customer);

        public Customer GetCustomerById(string customerId)
            => this.customerRepository.GetCustomerById(customerId);
    }
}
