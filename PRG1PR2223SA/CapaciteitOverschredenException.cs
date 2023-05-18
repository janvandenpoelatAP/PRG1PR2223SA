using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class CapaciteitOverschredenException : ApplicationException
    {
        public CapaciteitOverschredenException(string message) : base(message)
        {
        }
    }
}
