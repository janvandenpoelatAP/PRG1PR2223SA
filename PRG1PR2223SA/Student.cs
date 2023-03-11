using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG1PR2223SA
{
    internal class Student
    {
        public static uint Studententeller;
        public string Naam;
        public DateTime GeboorteDatum;
        public uint Studentennummer;
        private CursusResultaat[] cursusResultaten = new CursusResultaat[5];

        public string GenereerNaamkaarje()
        {
            return $"{this.Naam} (STUDENT)";
        }
        public byte BepaalWerkbelasting()
        {
            // Enkel de cursussen meenemen die zijn ingevuld (anders altijd 50!)
            byte werkbelasting = 0;
            for (int i = 0; i < this.cursusResultaten.Length; i++)
            {
                if (this.cursusResultaten[i] is not null)
                {
                    werkbelasting += 10;
                }
            }
            return werkbelasting;
        }
        public void Kwoteer(byte cursusIndex, byte behaaldCijfer)
        {
            // Controleer of index voldoet aan voorwaarden
            if (cursusIndex < 0 || cursusIndex > this.cursusResultaten.Length)
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
                this.cursusResultaten[cursusIndex].Resultaat = behaaldCijfer;
            } 
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            for (int i = 0; i < this.cursusResultaten.Length; i++)
            {
                if (this.cursusResultaten[i] is not null)
                {
                    som += this.cursusResultaten[i].Resultaat;
                    aantalCursussen++;
                }
            }
            // Opgelet, als student niet is ingeschreven in een cursus zal programma crashen. Probeer maar.
            return som / aantalCursussen;
        }
        public void RegistreerCursusResultaat(string naam, byte resultaat)
        {
            bool legePlaatsGevonden = false; // wordt true als lege plaats gevonden
            for (int i = 0; i < this.cursusResultaten.Length && !legePlaatsGevonden; i++)
            {
                if (cursusResultaten[i] is null)
                {
                    cursusResultaten[i] = new CursusResultaat(naam , resultaat); ;
                    legePlaatsGevonden = true;
                }
            }
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
            for (int i = 0; i < this.cursusResultaten.Length; i++)
            {
                if (this.cursusResultaten[i] is not null)
                {
                    Console.WriteLine($"{this.cursusResultaten[i].Naam}:\t{this.cursusResultaten[i].Resultaat}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public static void DemonstreerStudenten()
        {
            Student student1 = new Student();
            student1.Naam = "Said Aziz";
            student1.GeboorteDatum = new DateTime(2001, 1, 3);
            student1.RegistreerCursusResultaat("Communicatie", 12);
            student1.RegistreerCursusResultaat("Programmeren", 15);
            student1.RegistreerCursusResultaat("Webtechnologie", 13);
            student1.ToonOverzicht();

            Student student2 = new Student();
            student2.Naam = "Mieke Vermeulen";
            student2.GeboorteDatum = new DateTime(1998, 1, 1);
            student2.RegistreerCursusResultaat("Communicatie", 13);
            student2.RegistreerCursusResultaat("Programmeren", 16);
            student2.RegistreerCursusResultaat("Webtechnologie", 14);
            student2.ToonOverzicht();
        }
    }
}
