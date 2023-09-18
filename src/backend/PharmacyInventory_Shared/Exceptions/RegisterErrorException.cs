using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyInventory_Shared.Exceptions
{
    public class RegisterErrorException : Exception
    {
        
    public RegisterErrorException()
        {
        }

        public RegisterErrorException(string message)
            : base(message)
        {
        }

        public RegisterErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

