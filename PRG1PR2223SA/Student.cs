using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class Student
    {
        public static uint Studententeller;
        private static List<Student> alleStudenten = new List<Student>();
        public static List<Student> AlleStudenten
        {
            get
            {
                return alleStudenten;
            }
        }
        public string Naam;
        public DateTime Geboortedatum;
        public uint Studentennummer;
        private VakInschrijving[] vakInschrijvingen = new VakInschrijving[5];
        public Student(string naam, DateTime geboorteDatum)
        {
            Naam = naam;
            Geboortedatum = geboorteDatum;
            AlleStudenten.Add(this);
        }
        public string GenereerNaamkaarje()
        {
            return $"{this.Naam} (STUDENT)";
        }
        public byte BepaalWerkbelasting()
        {
            // Enkel de cursussen meenemen die zijn ingevuld (anders altijd 50!)
            byte werkbelasting = 0;
            for (int i = 0; i < this.vakInschrijvingen.Length; i++)
            {
                if (this.vakInschrijvingen[i] is not null)
                {
                    werkbelasting += 10;
                }
            }
            return werkbelasting;
        }
        public void Kwoteer(byte cursusIndex, byte behaaldCijfer)
        {
            // Controleer of index voldoet aan voorwaarden
            if (cursusIndex < 0 || cursusIndex > this.vakInschrijvingen.Length)
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
                this.vakInschrijvingen[cursusIndex].Resultaat = behaaldCijfer;
            } 
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            for (int i = 0; i < this.vakInschrijvingen.Length; i++)
            {
                if (this.vakInschrijvingen[i] is not null)
                {
                    if (vakInschrijvingen[i].Resultaat is not null)
                    {
                        som += (byte)this.vakInschrijvingen[i].Resultaat;
                        aantalCursussen++;
                    }
                }
            }
            // Opgelet, als student niet is ingeschreven in een cursus zal programma crashen. Probeer maar.
            return som / aantalCursussen;
        }
        public void RegistreerVakInschrijving(Cursus cursus, byte? resultaat)
        {
            bool legePlaatsGevonden = false; // wordt true als lege plaats gevonden
            for (int i = 0; i < this.vakInschrijvingen.Length && !legePlaatsGevonden; i++)
            {
                if (vakInschrijvingen[i] is null)
                {
                    vakInschrijvingen[i] = new VakInschrijving(cursus , resultaat); ;
                    legePlaatsGevonden = true;
                }
            }
        }
        public void ToonOverzicht()
        {
            DateTime nu = DateTime.Now;
            int aantalJaar = nu.Year - this.Geboortedatum.Year - 1;
            if (nu.Month > Geboortedatum.Month || nu.Month == Geboortedatum.Month && nu.Day >= Geboortedatum.Day)
            {
                aantalJaar++;
            }
            Console.WriteLine($"{Naam}, {aantalJaar} jaar");
            Console.WriteLine();
            Console.WriteLine("Cijferrapport");
            Console.WriteLine("*************");
            foreach (VakInschrijving vakInschrijving in vakInschrijvingen)
            {
                if (vakInschrijving is not null)
                {
                    Console.WriteLine($"{vakInschrijving.Cursus.Titel}:\t{vakInschrijving.Resultaat}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public static Student StudentUitTekstFormaat(string csvWaarde)
        {
            string[] studentInfo = csvWaarde.Split(';');
            Student student = new Student(studentInfo[0], new DateTime(Convert.ToInt32(studentInfo[3]), Convert.ToInt32(studentInfo[2]), Convert.ToInt32(studentInfo[1])));
            for (int i = 4; i < studentInfo.Length; i += 2)
            {
                Cursus cursus = new Cursus(studentInfo[i]);
                student.RegistreerVakInschrijving(cursus, Convert.ToByte(studentInfo[i + 1]));
            }
            return student;
        }
        public static void DemonstreerStudenten()
        {
            Cursus communicatie = new Cursus("Communicatie", new Student[2]);
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", new Student[5], 6);
            Cursus databanken = new Cursus("Databanken", new Student[7], 5);

            Student student1 = new Student("Said Aziz", new DateTime(2001, 1, 3));
            student1.RegistreerVakInschrijving(communicatie, 12);
            student1.RegistreerVakInschrijving(programmeren, null);
            student1.RegistreerVakInschrijving(webtechnologie, 13);
            student1.ToonOverzicht();

            Student student2 = new Student("Mieke Vermeulen", new DateTime(1998, 1, 1));
            student2.RegistreerVakInschrijving(communicatie, 13);
            student2.RegistreerVakInschrijving(programmeren, null);
            student2.RegistreerVakInschrijving(databanken, 14);
            student2.ToonOverzicht();
        }
        public static void DemonstreerStudentUitTekstFormaat()
        {
            Console.WriteLine("Geef de tekstvoorstelling van 1 student in CSV-formaat:");
            string csvWaarde = Console.ReadLine();
            Student student = Student.StudentUitTekstFormaat(csvWaarde);
            student.ToonOverzicht();
        }
    }
}
