using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAsyncManager customerManager;
        private readonly IAccountManager accountManager;

        public CustomerController(ICustomerAsyncManager customerManager,IAccountManager accountManager)
        {
            this.customerManager = customerManager;
            this.accountManager = accountManager;
        }
        //GET: /api/Customer/GetMiniStatement/SA-000007
        /// <summary>
        /// Returns Mini statement
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A list of last 5 transactions</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET: /api/Customer/GetMiniStatement/SA-000007
        /// </remarks>
        [HttpGet]
        [Authorize]
        [Route("/api/Customer/GetMiniStatement/{id}")]  
        public async Task<ActionResult> GetMiniStatement(string id)
        {
            int accountId = 0;
            try
            {
                accountId = this.customerManager.GetAccountIdByNumber(id);
                var account=this.accountManager.GetAccountById(accountId);
                
                if (accountId == 0)
                {
                    return NotFound($"Account with Account Number:{id} not found");
                }
                if (account.CustomerId != TokenRequest.CustomerId)
                {
                    return BadRequest("You don't have the permission to access the account");
                }
                var miniStatement = await this.customerManager.GetMiniStatement(accountId);
                if (miniStatement == null)
                {
                    return NotFound();
                }
                return Ok(miniStatement);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving transaction records from server");
            }

        }
        //GET: /api/Customer/Checkbalance/SB-000007
        /// <summary>
        /// Returns account balance
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Account Balance</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET: /api/Customer/Checkbalance/SB-000007
        /// </remarks>
        [Route("/api/Customer/Checkbalance/{accountNumber}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Checkbalance(string accountNumber)
        {
            int accountId = 0;
            try
            {
                accountId = this.customerManager.GetAccountIdByNumber(accountNumber);
                var account = this.accountManager.GetAccountById(accountId);
                
                if (accountId == 0)
                {
                    return NotFound($"Account with Account Number:{accountNumber} not found");
                }
                if (account.CustomerId != TokenRequest.CustomerId)
                {
                    return BadRequest("You don't have the permission to access the account");
                }
                double amount = await this.customerManager.GetBalance(accountId);
                return Ok(amount);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving Account Balance from server");
            }

        }

        //GET: /api/Customer/GetCustomizedStatement
        /// <summary>
        /// Returns a Customized statement
        /// </summary>
        /// <param name="customizedStatement"></param>
        /// <returns> A list of customized transactions</returns>
        /// /// <remarks>
        /// Sample Request:
        /// 
        ///     GET: /api/Customer/GetCustomizedStatement
        /// </remarks>
        [Route("/api/Customer/GetCustomizedStatement")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetCustomizedStatement(CustomizedStatement customizedStatement)
        {
            int accountId = 0;
            try
            {
                accountId = this.customerManager.GetAccountIdByNumber(customizedStatement.AccountNumber);
                var account = this.accountManager.GetAccountById(accountId);
                
                if (accountId == 0)
                {
                    return NotFound($"Account with Account Number:{customizedStatement.AccountNumber} not found");
                }
                if (account.CustomerId != TokenRequest.CustomerId)
                {
                    return BadRequest("You don't have the permission to access the account");
                }
                customizedStatement.AccountId = accountId;
                var customizedStatements = await this.customerManager.GetCustomizedStatement(customizedStatement);
                if (customizedStatements == null)
                {
                    return NotFound();
                }
                return Ok(customizedStatements);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Retrieving transaction records from server");
            }

        }
        //POST: /api/Customer/FundTransfer
        /// <summary>
        /// Returns a new transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>new transaction</returns>
        /// /// /// <remarks>
        /// Sample Request:
        /// 
        ///     POST: /api/Customer/FundTransfer
        /// </remarks>
        [Route("/api/Customer/FundTransfer")]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> FundTransfer(Transaction transaction)
        {
            int sAccountId = 0, dAccountId = 0;
            try
            {
                sAccountId = this.customerManager.GetAccountIdByNumber(transaction.SourseAccountNumber);
                dAccountId = this.customerManager.GetAccountIdByNumber(transaction.DestinationAccountNumber);
                if (sAccountId == 0 && dAccountId != 0)
                {
                    return BadRequest($"Account with Account Number:{transaction.SourseAccountNumber} not found");
                }
                if (sAccountId != 0 && dAccountId == 0)
                {
                    return BadRequest($"Account with Account Number:{transaction.DestinationAccountNumber} not found");
                }
                var account = this.accountManager.GetAccountById(sAccountId);
                if (account.CustomerId != TokenRequest.CustomerId)
                {
                    return BadRequest("You don't have the permission to access the account");
                }
                if (sAccountId==dAccountId)
                {
                    return BadRequest("Source and Destination cannot be same");
                }
                if (this.accountManager.GetAccountById(sAccountId).Balance <= transaction.TransactionAmount)
                {
                    return BadRequest("No Suffient balance");
                }
                transaction.SourceAccountNo = sAccountId;
                transaction.DestinationAccountNo = dAccountId;
                var result = await this.customerManager.FundTransfer(transaction);
                if (result == null)
                {
                    return BadRequest("Unable to Complete the transaction");
                }
                return Ok($"Amount:{transaction.TransactionAmount} is Transfered from {transaction.SourseAccountNumber} to {transaction.DestinationAccountNumber}");

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Performing Transaction");
            }
        }
    }
}
