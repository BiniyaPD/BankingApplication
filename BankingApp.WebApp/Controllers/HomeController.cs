using BankingApp.BusinessLayer.Contracts;
using BankingApp.CommonLayer.Models;
using BankingApp.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerManager customerManager;
        private readonly IBankManager bankManager;

        public HomeController(ILogger<HomeController> logger,ICustomerManager customerManager,IBankManager bankManager)
        {
            _logger = logger;
            this.customerManager = customerManager;
            this.bankManager = bankManager;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ManagerLogin login)
        {
            if(ModelState.IsValid)
            {
                var manager = this.bankManager.GetManagerById(login.LoginId);
                if(manager!=null)
                {
                    if(manager.ManagerPassword==login.Password)
                    {
                        HttpContext.Session.SetString("ManagerId", login.LoginId);
                        return RedirectToAction("Index", "Home");                       
                    }
                }
                ModelState.AddModelError("", "Invalid Login ID or password");
            }
            return View(login);
        }
        
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            bool isUpdated = false;
            if (ModelState.IsValid)
            {
                string managerId = HttpContext.Session.GetString("ManagerId");
                var managerDb = this.bankManager.GetManagerById(managerId);
                if(managerDb.ManagerPassword==changePassword.OldPassword && managerDb.ManagerId==managerId)
                {
                    var manager = new Manager()
                    {
                        ManagerId=managerId,
                        ManagerPassword = changePassword.NewPassword
                    };
                    isUpdated=this.bankManager.UpdatePassword(manager);
                    if (isUpdated == true)
                    {
                        TempData["Message"] = $"Password Changed Successfully";
                        return RedirectToAction("DisplayMessage", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Password");
                }
                
            }
            return View();
        }
        public IActionResult DisplayMessage()
        {
            ViewBag.msg = TempData["Message"];
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
