using BankingApp.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EFLayer.Contracts
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Method to add customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        string AddCustomer(Customer customer);
        /// <summary>
        /// Method to edit customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        bool EditCustomer(Customer customer);
        /// <summary>
        /// Method to delete customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        bool DeleteCustomer(string customerId);

        /// <summary>
        /// Method to Get the customer details based on id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer GetCustomerById(string customerId);

    }
}
