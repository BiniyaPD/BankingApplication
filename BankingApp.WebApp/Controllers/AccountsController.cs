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
    public class AccountsController : Controller
    {
        private readonly IAccountManager accountManager;
        private readonly ICustomerManager customerManager;

        public AccountsController(IAccountManager accountManager, ICustomerManager customerManager)
        {
            this.accountManager = accountManager;
            this.customerManager = customerManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddAccount()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddAccount(AccountVM accountVM)
        {
            string accountNumber = string.Empty;
            if(ModelState.IsValid)
            {
                var customer = this.customerManager.GetCustomerById(accountVM.CustomerId);
                string managerId = HttpContext.Session.GetString("ManagerId");
                if (customer!=null && customer.ManagerId==managerId)
                {
                    if (accountVM.AccountType == "Savings")
                    {
                        if(accountVM.Balance<1000)
                        {
                            ModelState.AddModelError("", $"Should Deposit the minimum balance");
                            return View();
                        }
                        SavingsAccount savingsAccount = new SavingsAccount()
                        {
                            MinimumBalance = 1000,
                            Account = new Account()
                            {
                                CustomerId = accountVM.CustomerId,
                                Balance = accountVM.Balance,
                                AccountType = accountVM.AccountType,
                                Tin = accountVM.Tin,
                                Doc = DateTime.UtcNow,
                                Ifsc = "HDFC00021"
                            }

                        };
                        accountNumber = this.accountManager.AddSavingsAccount(savingsAccount);
                    }
                    else if(accountVM.AccountType=="Corporate")
                    {
                        if (accountVM.Balance <=0)
                        {
                            ModelState.AddModelError("", $"Should Deposit the minimum balance");
                            return View();
                        }
                        CorporateAccount corporateAccount = new CorporateAccount()
                        {
                            
                            MinimumBalance = 0,
                            Account = new Account()
                            {
                                CustomerId = accountVM.CustomerId,
                                Balance = accountVM.Balance,
                                AccountType = accountVM.AccountType,
                                Tin = accountVM.Tin,
                                Doc = DateTime.UtcNow,
                                Ifsc = "HDFC00021"
                            }
                        };
                        accountNumber = this.accountManager.AddCorporateAccount(corporateAccount);
                    }
                    else
                    {
                        if (accountVM.Balance < 5000)
                        {
                            ModelState.AddModelError("", $"Should Deposit the minimum balance");
                            return View();
                        }
                        CurrentAccount currentAccount = new CurrentAccount()
                        {
                            
                            MinimumBalance = 5000,
                            TinNumber=accountVM.TinNumber,
                            Account = new Account()
                            {
                                CustomerId = accountVM.CustomerId,
                                Balance = accountVM.Balance,
                                AccountType = accountVM.AccountType,
                                Tin = accountVM.Tin,
                                Doc = DateTime.UtcNow,
                                Ifsc = "HDFC00021"
                            }
                        };
                        accountNumber = this.accountManager.AddCurrentAccount(currentAccount);
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Customer with CustomerId:{accountVM.CustomerId} does not exist or You Dont have the permission to add account for this customer");
                    //ModelState.AddModelError("", $"You Dont have the permission to add account for this customer");
                }
                if(accountNumber != string.Empty)
                {
                    TempData["Message"] = $"Account With Account Number:{accountNumber} Created Successfully";
                    return RedirectToAction("DisplayMessage", "Home");
                }
                else
                {
                    ModelState.AddModelError("", $"Failed to Create Account.The customer already have the same type of account");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteAccount()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult DeleteAccount(AccountNumberSubmit accountNumberSubmit)
        {
            int accountId = 0;
            bool isDeleted = false;
            if (ModelState.IsValid)
            {
                accountId = this.accountManager.GetAccountIdByNumber(accountNumberSubmit.AccountNumber);
                if(accountId!=0)
                {
                    isDeleted=this.accountManager.DeleteAccount(accountNumberSubmit.AccountNumber);
                    if(isDeleted==true)
                    {
                        TempData["Message"] = $"Account With Account Number:{accountNumberSubmit.AccountNumber} Deleted Successfully";
                        return RedirectToAction("DisplayMessage", "Home");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", $"Account With Account Number:{accountNumberSubmit.AccountNumber} is not exist");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditAccountNumberSubmit()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult EditAccountNumberSubmit(AccountNumberSubmit accountNumberSubmit)
        {
            int accountId = 0;
            if(ModelState.IsValid)
            {
                accountId = this.accountManager.GetAccountIdByNumber(accountNumberSubmit.AccountNumber);
                if(accountId!=0)
                {
                    var account = this.accountManager.GetAccountById(accountId);
                    TempData["accountId"] = accountId;
                    return RedirectToAction("EditAccount", account);
                }
                ModelState.AddModelError("", $"Account With Account Number:{accountNumberSubmit.AccountNumber} is not exist");                
            }
            return View();
        }
        [HttpGet]
        public IActionResult EditAccount(AccountVM account)
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View(account);
        }
        [HttpPost]
        public IActionResult EditAccountDetails(AccountVM accountVM)
        {
            bool isUpdated = false;
            var accountDb = this.accountManager.GetAccountById((int)TempData["accountId"]);
            if(ModelState.IsValid)
            {
                if(accountDb.AccountType!=accountVM.AccountType)
                {
                    var account = new Account()
                    {
                        AccountNumber = (int)TempData["accountId"],
                        Balance = accountVM.Balance,
                        AccountType=accountVM.AccountType,
                        Tin = accountVM.Tin
                    };
                    isUpdated = this.accountManager.EditAccount(account);
                    if(isUpdated==true)
                    {
                        TempData["Message"] = $"Account With Account Number:{TempData["accountId"]} Updated Successfully";
                        return RedirectToAction("DisplayMessage", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Updation Of Account Failed");
                }
                
            }
            return RedirectToAction("EditAccountNumberSubmit");
        }
    }
}
