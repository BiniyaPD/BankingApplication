using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.CommonLayer.CustomExceptions
{
    public class CustomerNotFoundException:ApplicationException
    {
        public CustomerNotFoundException()
        {

        }
        public CustomerNotFoundException(string errorMsg)
            :base(errorMsg)
        {

        }
        public CustomerNotFoundException(string errorMsg,Exception innerException)
            :base(errorMsg,innerException)
        {

        }
    }
}
