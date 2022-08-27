using BankingApp.CommonLayer.CustomExceptions;
using BankingApp.CommonLayer.Models;
using BankingApp.EFLayer.Contracts;
using BankingApp.EFLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EFLayer.Implementation
{
    public class CustomerRepositoryEFImpl : ICustomerRepository
    {
        private readonly db_bankContext context;

        public CustomerRepositoryEFImpl(db_bankContext context)
        {
            this.context = context;
        }

        public string AddCustomer(CommonLayer.Models.Customer customer)
        {
            string customerId = string.Empty;
            try
            {
                var customerDb = new Models.Customer()
                {
                    ManagerId = customer.ManagerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Gender = customer.Gender,
                    Dob = customer.Dob,
                    EmailId = customer.EmailId,
                    MobileNumber = customer.MobileNumber,
                    City = customer.City,
                    State = customer.State,
                    Pincode = customer.Pincode,
                    IsDeleted = false
                };
                this.context.Add(customerDb);
                this.context.SaveChanges();
                customerId = customerDb.CustomerId;
            }
            catch (Exception)
            {

                throw;
            }
            return customerId;
        }

        public bool DeleteCustomer(string customerId)
        {
            bool isDeleted = false;
            try
            {
                var customerToDelete = this.context.Customers.FirstOrDefault(x => x.CustomerId == customerId);
                if (customerToDelete == null)
                {
                    throw new CustomerNotFoundException($"Customer with {customerId} does not exist");
                }
                customerToDelete.IsDeleted = true;
                this.context.SaveChanges();
                isDeleted = true;
            }
            catch (Exception)
            {

                throw;
            }
            return isDeleted;
        }

        public bool EditCustomer(CommonLayer.Models.Customer customer)
        {
            bool isUpdated = false;
            try
            {
                var customerToUpdate = this.context.Customers.FirstOrDefault(x => x.CustomerId == customer.CustomerId);
                if (customerToUpdate == null)
                {
                    throw new CustomerNotFoundException($"Customer with {customer.CustomerId} does not exist");
                }

                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.Gender = customer.Gender;
                customerToUpdate.Dob = customer.Dob;
                customerToUpdate.EmailId = customer.EmailId;
                customerToUpdate.MobileNumber = customer.MobileNumber;
                customerToUpdate.City = customer.City;
                customerToUpdate.State = customer.State;
                customerToUpdate.Pincode = customer.Pincode;
                this.context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            return isUpdated;
        }

        public CommonLayer.Models.Customer GetCustomerById(string customerId)
        {
            CommonLayer.Models.Customer customer = null;
            try
            {
                var customerDb = this.context.Customers.FirstOrDefault(x => x.CustomerId == customerId && x.IsDeleted==false);
                if (customerDb != null)
                {
                    customer = new CommonLayer.Models.Customer()
                    {
                        CustomerId = customerDb.CustomerId,
                        ManagerId = customerDb.ManagerId,
                        FirstName = customerDb.FirstName,
                        LastName = customerDb.LastName,
                        Gender = customerDb.Gender,
                        Dob = customerDb.Dob,
                        EmailId = customerDb.EmailId,
                        MobileNumber = customerDb.MobileNumber,
                        City = customerDb.City,
                        State = customerDb.State,
                        Pincode = customerDb.Pincode,
                        IsDeleted = customerDb.IsDeleted
                    };

                }
            }
            catch (Exception)
            {

                throw;
            }
            return customer;
        }
    }
}
