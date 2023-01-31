using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BaseDomainException : Exception
    {
        public BaseDomainException(string message) : base(message)
        {
        }
    }
}