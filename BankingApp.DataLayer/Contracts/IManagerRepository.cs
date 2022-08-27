using BankingApp.CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.EFLayer.Contracts
{
    public interface IManagerRepository
    {
        /// <summary>
        /// Method to get the Manager details by Id
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        Manager GetManagerById(string managerId);
        /// <summary>
        /// Method to update the manager password
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        bool UpdatePassword(Manager manager);
    }
}
