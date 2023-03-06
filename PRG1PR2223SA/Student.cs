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
        private string[] cursussen = new string[5];

        public string GenereerNaamkaarje()
        {
            return $"{this.Naam} (STUDENT)";
        }
        // Merk op dat cursussen private is en dus nog enkel vanuit de klasse kan worden aangesproken.
        public void RegistreerVoorCursus(string cursus)
        {
            /*
            // Quick and dirty met break;
            for (int i = 0; i < this.cursussen.Length; i++)
            {
                if (this.cursussen[i] is null)
                {
                    this.cursussen[i] = cursus;
                    break; // Anders wordt de cursus nog in de volgende lege plaatsen gezet!
                }
            }
            */
            // Netjes met extra conditie in for loop
            bool legePlaatsGevonden = false; // wordt true als lege plaats gevonden
            for (int i = 0; i < cursussen.Length && !legePlaatsGevonden; i++)
            {
                if (cursussen[i] is null)
                {
                    cursussen[i] = cursus;
                    legePlaatsGevonden = true;
                }
            }
            // Als legePlaatsGevonden nog false is kan je een foutboodschap schrijven
        }
        public double BepaalWerkbelasting()
        {
            // Enkel de cursussen meenemen die zijn ingevuld (anders altijd 50!)
            double werkbelasting = 0.0;
            for (int i = 0; i < this.cursussen.Length; i++)
            {
                if (cursussen[i] is not null)
                {
                    werkbelasting += 10.0;
                }
            }
            return werkbelasting;
        }

    }
}
