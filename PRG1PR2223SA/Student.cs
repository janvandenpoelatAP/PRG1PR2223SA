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
        public byte[] CursusResultaten = new byte[5];

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
            for (int i = 0; i < this.cursussen.Length && !legePlaatsGevonden; i++)
            {
                if (this.cursussen[i] is null)
                {
                    this.cursussen[i] = cursus;
                    legePlaatsGevonden = true;
                }
            }
            // Als legePlaatsGevonden nog false is kan je een foutboodschap schrijven
        }
        public byte BepaalWerkbelasting()
        {
            // Enkel de cursussen meenemen die zijn ingevuld (anders altijd 50!)
            byte werkbelasting = 0;
            for (int i = 0; i < this.cursussen.Length; i++)
            {
                if (this.cursussen[i] is not null)
                {
                    werkbelasting += 10;
                }
            }
            return werkbelasting;
        }
        public void Kwoteer(byte cursusIndex, byte behaaldCijfer)
        {
            // Controleer of index voldoet aan voorwaarden
            if (cursusIndex < 0 || cursusIndex > this.cursussen.Length)
            {
                Console.WriteLine("Ongeldige cursus index!");
            }
            // Controleer of resultaat voldoet aan voorwaarden
            else if (behaaldCijfer < 0 || behaaldCijfer > 20)
            {
                Console.WriteLine("Behaald cijfer moet tussen 0 en 20 liggen!");
            }
            else
            {
                this.CursusResultaten[cursusIndex] = behaaldCijfer;
            } 
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            for (int i = 0; i < cursussen.Length; i++)
            {
                if (cursussen[i] is not null)
                {
                    som += CursusResultaten[i];
                    aantalCursussen++;
                }
            }
            // Opgelet, als student niet is ingeschreven in een cursus zal programma crashen. Probeer maar.
            return som / aantalCursussen;
        }
        public void ToonOverzicht()
        {
            DateTime nu = DateTime.Now;
            int aantalJaar = nu.Year - this.GeboorteDatum.Year - 1;
            if (nu.Month > GeboorteDatum.Month || nu.Month == GeboorteDatum.Month && nu.Day >= GeboorteDatum.Day)
            {
                aantalJaar++;
            }
            Console.WriteLine($"{Naam}, {aantalJaar} jaar");
            Console.WriteLine();
            Console.WriteLine("Cijferrapport");
            Console.WriteLine("*************");
            for (int i = 0; i < cursussen.Length; i++)
            {
                if (cursussen[i] is not null)
                {
                    Console.WriteLine($"{cursussen[i]}:\t{CursusResultaten[i]}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public static void DemonstreerStudenten()
        {
            Student student1 = new Student();
            student1.Naam = "Said Aziz";
            student1.GeboorteDatum = new DateTime(2001, 1, 3);
            student1.RegistreerVoorCursus("Communicatie");
            student1.CursusResultaten[0] = 12;
            student1.RegistreerVoorCursus("Programmeren");
            student1.CursusResultaten[1] = 15;
            student1.RegistreerVoorCursus("Webtechnologie");
            student1.CursusResultaten[2] = 13;
            student1.ToonOverzicht();

            Student student2 = new Student();
            student2.Naam = "Mieke Vermeulen";
            student2.GeboorteDatum = new DateTime(1998, 1, 1);
            student2.RegistreerVoorCursus("Communicatie");
            student2.CursusResultaten[0] = 13;
            student2.RegistreerVoorCursus("Programmeren");
            student2.CursusResultaten[1] = 16;
            student2.RegistreerVoorCursus("Databanken");
            student2.CursusResultaten[2] = 14;
            student2.ToonOverzicht();
        }
    }
}
