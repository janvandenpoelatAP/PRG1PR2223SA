using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class Student:Persoon
    {
        public static ImmutableList<Student> AlleStudenten
        {
            get
            {
                var enkelStudenten = new List<Student>();
                foreach (var persoon in Persoon.AllePersonen)
                {
                    if (persoon is Student)
                    {
                        enkelStudenten.Add((Student)persoon);
                    }
                }
                return enkelStudenten.ToImmutableList<Student>();
            }
        }
        private List<VakInschrijving> vakInschrijvingen = new List<VakInschrijving>();
        private Dictionary<DateTime, string> dossier;
        public ImmutableDictionary<DateTime, string> Dossier
        {
            get
            {
                return this.dossier.ToImmutableDictionary<DateTime, string>();
            }
        }
        public Student(string naam, DateTime geboortedatum):base(naam, geboortedatum)
        {
            this.dossier = new Dictionary<DateTime, string>();
            AlleStudenten.Add(this);
        }
        public void Kwoteer(byte cursusIndex, byte behaaldCijfer)
        {
            // Controleer of index voldoet aan voorwaarden
            if (cursusIndex < 0 || cursusIndex > this.vakInschrijvingen.Count)
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
            foreach (VakInschrijving vakInschrijving in vakInschrijvingen)
            {
                if (vakInschrijving.Resultaat is not null)
                {
                    som += (byte)vakInschrijving.Resultaat;
                    aantalCursussen++;
                }
            }
            // Opgelet, als student niet is ingeschreven in een cursus zal programma crashen. Probeer maar.
            return som / aantalCursussen;
        }
        public void RegistreerVakInschrijving(Cursus cursus, byte? resultaat)
        {
            vakInschrijvingen.Add(new VakInschrijving(cursus, resultaat));
        }
        public override double BepaalWerkbelasting()
        {
            double werkbelasting = 0.0;
            foreach (VakInschrijving vakinschrijving in vakInschrijvingen)
            {
                werkbelasting += 10;
            }
            return werkbelasting;
        }
        public override string GenereerNaamkaartje()
        {
            return $"{Naam} (STUDENT)";
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
            Cursus communicatie = new Cursus("Communicatie", new List<Student>());
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", new List<Student>(), 6);
            Cursus databanken = new Cursus("Databanken", new List<Student>(), 5);

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
