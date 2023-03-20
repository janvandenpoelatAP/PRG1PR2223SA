using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class VakInschrijving
    {
        private string naam;
        public string Naam
        {
            get
            {
                return naam;
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
        public VakInschrijving(string naam, byte? resultaat)
        {
            this.naam = naam;
            this.Resultaat = resultaat;
        }
    }
}
