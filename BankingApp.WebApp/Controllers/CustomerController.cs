using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using BankingApp.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerManager customerManager;
        private readonly IAccountManager accountManager;

        public CustomerController(ICustomerManager customerManager,IAccountManager accountManager)
        {
            this.customerManager = customerManager;
            this.accountManager = accountManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddCustomer(CustomerVM customerVM)
        {
            string customerId = string.Empty;
            if(ModelState.IsValid)
            {
                if(customerVM.Dob>DateTime.Now)
                {
                    ModelState.AddModelError("", "Please select a valid date");
                    return View();
                }
                var customer = new Customer()
                {
                    ManagerId = HttpContext.Session.GetString("ManagerId"),
                    FirstName = customerVM.FirstName,
                    LastName = customerVM.LastName,
                    Dob = customerVM.Dob,
                    EmailId = customerVM.EmailId,
                    Gender=customerVM.Gender,
                    MobileNumber = customerVM.MobileNumber,
                    City = customerVM.City,
                    State = customerVM.State,
                    Pincode = customerVM.Pincode
                };
                customerId = this.customerManager.AddCustomer(customer);
            }
            if(customerId != string.Empty)
            {
                
                TempData["Message"] = $"Customer With CustomerID:{customerId} Added Successfully";
                return RedirectToAction("DisplayMessage", "Home");
            }          
            return View();
        }

        [HttpGet]
        public IActionResult EditCustomerIdSubmit()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditCustomerIdSubmit(CustomerIdSubmit editcustomerIdSubmit)
        {
            if(ModelState.IsValid)
            {
                var customer = this.customerManager.GetCustomerById(editcustomerIdSubmit.CustomerId);
                if(customer!=null)
                {
                    TempData["customerId"] = editcustomerIdSubmit.CustomerId;
                    return RedirectToAction("EditCustomer","Customer",customer);
                }
                ModelState.AddModelError("",$"Customer with CustomerId:{editcustomerIdSubmit.CustomerId} does not exist");
            }
            return View(editcustomerIdSubmit);
        }

        [HttpGet]
        public IActionResult EditCustomer(CustomerVM customer)
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult EditCustomerDetails(CustomerVM customerVM)
        {
            bool isUpdated = false;
            CommonLayer.Models.Customer customerDb = null;
            if(ModelState.IsValid)
            {
                if (customerVM.Dob <= DateTime.Now)
                {
                    string managerId = HttpContext.Session.GetString("ManagerId");
                    customerDb = this.customerManager.GetCustomerById(TempData["customerId"].ToString());
                    if (managerId == customerDb.ManagerId)
                    {
                        var customer = new Customer()
                        {
                            CustomerId = TempData["customerId"].ToString(),
                            //ManagerId = HttpContext.Session.GetString("ManagerId"),
                            FirstName = customerVM.FirstName,
                            LastName = customerVM.LastName,
                            Dob = customerVM.Dob,
                            EmailId = customerVM.EmailId,
                            Gender = customerVM.Gender,
                            MobileNumber = customerVM.MobileNumber,
                            City = customerVM.City,
                            State = customerVM.State,
                            Pincode = customerVM.Pincode
                        };
                        isUpdated = this.customerManager.EditCustomer(customer);
                        if (isUpdated == true)
                        {
                            TempData["Message"] = $"Customer Info of CustomerID:{customerDb.CustomerId} Updated Successfully";
                            return RedirectToAction("DisplayMessage", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", $"You Don't have the permission to Update this Customer Details");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Please select a valid date");
                    //return View("EditCustomer");
                    return RedirectToAction("EditCustomer");
                }
            }
                
            return RedirectToAction("EditCustomer");
        }

        [HttpGet]
        public IActionResult DeleteCustomer()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult DeleteCustomer(CustomerIdSubmit deletecustomerIdSubmit)
        {
            bool isDeleted = false;
            if (ModelState.IsValid)
            {
                string managerId = HttpContext.Session.GetString("ManagerId");
                var customer = this.customerManager.GetCustomerById(deletecustomerIdSubmit.CustomerId);
                if (customer != null)
                {
                    int accountCount = this.accountManager.GetAccountByCustomerId(customer.CustomerId).Count();
                    if(customer.ManagerId==managerId && accountCount==0)
                    {
                        isDeleted=this.customerManager.DeleteCustomer(deletecustomerIdSubmit.CustomerId);
                        if (isDeleted == true)
                        {
                            TempData["Message"] = $"Customer with CustomerID:{deletecustomerIdSubmit.CustomerId} Deleted Successfully";
                            return RedirectToAction("DisplayMessage", "Home");
                        }
                    }
                    else
                    {
                        //ModelState.AddModelError("", $"You Don't have the permission to Delete this Customer Details");
                        ModelState.AddModelError("", $"Customer Details Cannot be Deleted,The customer have Active bank account");
                    }                   
                }
                else
                {
                    ModelState.AddModelError("", $"Customer with CustomerId:{deletecustomerIdSubmit.CustomerId} does not exist");
                }              
            }

            return View();
        }


    }
}
