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
    public class TransactionsController : Controller
    {
        private readonly IAccountManager accountManager;
        private readonly ITransactionManager transactionManager;

        public TransactionsController(IAccountManager accountManager,ITransactionManager transactionManager)
        {
            this.accountManager = accountManager;
            this.transactionManager = transactionManager;
        }
        [HttpGet]
        public IActionResult Deposit()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Deposit(DepositWithdraw depositWithdraw)
        {
            int accountId = 0;
            bool isDeposited = false;
            if(ModelState.IsValid)
            {
                accountId = this.accountManager.GetAccountIdByNumber(depositWithdraw.AccountNumber);
                if (accountId != 0)
                {
                    var transaction = new CommonLayer.Models.Transaction()
                    {
                        SourceAccountNo=accountId,
                        TransactionAmount=depositWithdraw.Amount,
                        TransactionType="Deposit",
                        TransactionDescription=depositWithdraw.Description
                    };
                    isDeposited = this.transactionManager.Deposit(transaction);
                    if(isDeposited==true)
                    {
                        TempData["Message"] = $"Amount Deposited to Account:{depositWithdraw.AccountNumber}";
                        return RedirectToAction("DisplayMessage", "Home");
                    }
                   
                }
                else
                {
                    ModelState.AddModelError("", $"Account With Account Number:{depositWithdraw.AccountNumber} does not exist");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Withdraw()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Withdraw(DepositWithdraw depositWithdraw)
        {
            int accountId = 0;
            bool isWithdraw = false;
            if (ModelState.IsValid)
            {
                accountId = this.accountManager.GetAccountIdByNumber(depositWithdraw.AccountNumber);               
                if (accountId != 0)
                {
                    var account = this.accountManager.GetAccountById(accountId);
                    if(account.Balance>=depositWithdraw.Amount)
                    {
                        double totalTransactionAmount = this.transactionManager.DailyTransactionsAmount(accountId);
                        double amount = totalTransactionAmount + depositWithdraw.Amount;
                        if((account.AccountType=="Savings" && amount<=100000) ||(account.AccountType == "Current" && amount <= 500000) ||(account.AccountType == "Corporate" && amount <= 200000))
                        {
                            var transaction = new CommonLayer.Models.Transaction()
                            {
                                SourceAccountNo = accountId,
                                TransactionAmount = depositWithdraw.Amount,
                                TransactionType = "Withdraw",
                                TransactionDescription = depositWithdraw.Description
                            };
                            isWithdraw = this.transactionManager.WithDraw(transaction);
                            if (isWithdraw == true)
                            {
                                TempData["Message"] = $"Amount Withdraw from Account:{depositWithdraw.AccountNumber}";
                                return RedirectToAction("DisplayMessage", "Home");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Your daily transaction limit exceeded....");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "No Suffient Balance For this transaction");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Account With Account Number:{depositWithdraw.AccountNumber} does not exist");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult FundTransfer()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult FundTransfer(FundTransfer fundTransfer)
        {
            int sAccountId = 0,dAccountId=0;
            bool isTransfer = false;
            if (ModelState.IsValid)
            {
                sAccountId = this.accountManager.GetAccountIdByNumber(fundTransfer.SourceAccountNo);
                dAccountId = this.accountManager.GetAccountIdByNumber(fundTransfer.DestinationAccountNo);
                if (sAccountId != 0 && dAccountId!=0)
                {
                    if(sAccountId!=dAccountId)
                    {
                        var sAccount = this.accountManager.GetAccountById(sAccountId);
                        if (sAccount.Balance >= fundTransfer.Amount)
                        {
                            double totalTransactionAmount = this.transactionManager.DailyTransactionsAmount(sAccountId);
                            double amount = totalTransactionAmount + fundTransfer.Amount;
                            if ((sAccount.AccountType == "Savings" && amount <= 100000) || (sAccount.AccountType == "Current" && amount <= 500000) || (sAccount.AccountType == "Corporate" && amount <= 200000))
                            {
                                var transaction = new CommonLayer.Models.Transaction()
                                {
                                    SourceAccountNo = sAccountId,
                                    TransactionAmount = fundTransfer.Amount,
                                    TransactionType = "Transfer",
                                    DestinationAccountNo = dAccountId,
                                    TransactionDescription = fundTransfer.Description
                                };
                                isTransfer = this.transactionManager.Transfer(transaction);
                                if (isTransfer == true)
                                {
                                    TempData["Message"] = $"Amount:{fundTransfer.Amount} Transfered To {fundTransfer.DestinationAccountNo}";
                                    return RedirectToAction("DisplayMessage", "Home");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Your daily transaction limit exceeded....");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "No Suffient Balance For this transaction");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The Source Account Number and Destination Account Number Should be different");
                    }
                    
                }
                else
                {
                    //ModelState.AddModelError("", $"Account With Account Number:{fundTransfer.SourceAccountNo} does not exist");
                    ModelState.AddModelError("", $"Account does not exist");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult BalanceCheck()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult BalanceCheck(AccountNumberSubmit accountNumberSubmit)
        {
            int accountId = 0;
            if (ModelState.IsValid)
            {
                accountId = this.accountManager.GetAccountIdByNumber(accountNumberSubmit.AccountNumber);
                if (accountId != 0)
                {
                    var account = this.accountManager.GetAccountById(accountId);
                    if (account != null)
                    {
                        TempData["Amount"]= $"Account Balance:{account.Balance}";
                        return RedirectToAction("DisplayBalance");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Account With Account Number:{accountNumberSubmit.AccountNumber} does not exist");
                }
            }
            return View();
        }
        public IActionResult DisplayBalance()
        {
            ViewBag.balance = TempData["Amount"];
            return View();
        }
        [HttpGet]
        public IActionResult MiniStatement()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult MiniStatement(AccountNumberSubmit accountNumberSubmit)
        {
            int accountId = 0;
            IEnumerable<Transaction> transactions = null;
            if (ModelState.IsValid)
            {
                accountId = this.accountManager.GetAccountIdByNumber(accountNumberSubmit.AccountNumber);
                if (accountId != 0)
                {
                     transactions = this.transactionManager.GetMiniStatement(accountId);
                    
                }
                else
                {
                    ModelState.AddModelError("", $"Account With Account Number:{accountNumberSubmit.AccountNumber} does not exist");
                }
            }
            return View("DisplayMiniStatement", transactions);
        }
        [HttpGet]
        public IActionResult CustomizedStatement()
        {
            var managerId = HttpContext.Session.GetString("ManagerId");
            if (managerId == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult CustomizedStatement(Models.CustomizedStatement customizedStatement)
        {
            int accountId = 0;
            IEnumerable<Transaction> transactions = null;
            if (ModelState.IsValid)
            {
                accountId = this.accountManager.GetAccountIdByNumber(customizedStatement.AccountNumber);
                if (accountId != 0)
                {
                    if(customizedStatement.ToDate>=customizedStatement.FromDate)
                    {
                        var statement = new CommonLayer.Models.CustomizedStatement()
                        {
                            AccountId = accountId,
                            AccountNumber = customizedStatement.AccountNumber,
                            FromDate = customizedStatement.FromDate,
                            ToDate = customizedStatement.ToDate,
                            LowerLimit = customizedStatement.LowerLimit,
                            NoOfTransaction = customizedStatement.NoOfTransaction
                        };
                        transactions = this.transactionManager.GetCustomizedStatement(statement);
                    }
                    else
                    {
                        ModelState.AddModelError("", $"To date Should be Greater than From date");
                        return View();
                    }
                    

                }
                else
                {
                    ModelState.AddModelError("", $"Account With Account Number:{customizedStatement.AccountNumber} does not exist");
                    return View();
                }
            }
            return View("DisplayMiniStatement", transactions);
        }
    }
}
