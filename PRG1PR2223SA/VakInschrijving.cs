using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class VakInschrijving
    {
        private Cursus cursus;
        public Cursus Cursus
        {
            get
            {
                return cursus;
            }
        }
        private byte? resultaat;
        public byte? Resultaat
        {
            get
            {
                return resultaat;
            }
            set
            {
                if (value is null || !(value < 0 || value > 20))
                {
                    resultaat = value;
                }
            }
        }
        public VakInschrijving(Cursus cursus, byte? resultaat)
        {
            this.cursus = cursus;
            this.Resultaat = resultaat;
        }
    }
}
