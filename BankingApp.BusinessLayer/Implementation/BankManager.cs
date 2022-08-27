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
    public class BankManager : IBankManager
    {
        private readonly IManagerRepository managerRepository;

        public BankManager(IManagerRepository managerRepository)
        {
            this.managerRepository = managerRepository;
        }
        public Manager GetManagerById(string managerId)
            => this.managerRepository.GetManagerById(managerId);

        public bool UpdatePassword(Manager manager)
            => this.managerRepository.UpdatePassword(manager);
    }
}
