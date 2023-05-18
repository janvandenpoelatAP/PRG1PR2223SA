using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class DuplicateDataException : ApplicationException
    {
        private Object waarde1;
        public Object Waarde1
        {
            get { return waarde1; }
        }
        private Object waarde2;
        public Object Waarde2
        {
            get { return waarde2; }
        }
        public DuplicateDataException(string message, Object waarde1, Object waarde2) : base(message)
        {
            this.waarde1 = waarde1;
            this.waarde2 = waarde2;
        }
    }
}
