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
    public class ManagerRepositoryEFImpl : IManagerRepository
    {
        private readonly db_bankContext context;

        public ManagerRepositoryEFImpl(db_bankContext context)
        {
            this.context = context;
        }

        public bool UpdatePassword(CommonLayer.Models.Manager manager)
        {
            bool isUpdated = true;
            var managerDb = this.context.Managers.FirstOrDefault(x => x.ManagerId == manager.ManagerId);
            if(managerDb!=null)
            {
                managerDb.ManagerPassword = manager.ManagerPassword;
                this.context.SaveChanges();
                isUpdated = true;
            }
            return isUpdated;
        }

        CommonLayer.Models.Manager IManagerRepository.GetManagerById(string managerId)
        {
            CommonLayer.Models.Manager manager = null;
            var managerDb = this.context.Managers.FirstOrDefault(x => x.ManagerId == managerId);
            if(managerDb!=null)
            {
                manager = new CommonLayer.Models.Manager()
                {
                    ManagerId = managerDb.ManagerId,
                    FirstName = managerDb.FirstName,
                    LastName = managerDb.LastName,
                    Gender = managerDb.Gender,
                    Dob = managerDb.Dob,
                    ManagerPassword = managerDb.ManagerPassword,
                    EmailId = managerDb.EmailId,
                    MobileNumber = managerDb.MobileNumber
                };
            }
            return manager;
        }
    }
}
