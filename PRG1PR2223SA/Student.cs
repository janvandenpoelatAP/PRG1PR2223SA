using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG1PR2223SA
{
    internal class Student
    {
        public static uint Sudententeller;
        public string Naam;
        public DateTime GeboorteDatum;
        public uint Sudentennummer;
        public string[] Cursussen;

        public string GenereerNaamkaarje()
        {
            return $"{this.Naam} (STUDENT)";
        }
        public double BepaalWerkbelasting()
        {
            // Enkel de cursussen meenemen die zijn ingevuld (anders altijd 50!)
            double werkbelasting = 0.0;
            for (int i = 0; i < this.Cursussen.Length; i++)
            {
                if (Cursussen[i] is not null)
                {
                    werkbelasting += 10.0;
                }
            }
            return werkbelasting;
        }

    }
}
