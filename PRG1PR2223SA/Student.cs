using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Student : Persoon
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
        public ImmutableList<VakInschrijving> VakInschrijvingen
        {
            get
            {
                var enkelVoorDezeStudent = new List<VakInschrijving>();
                foreach (var inschrijving in VakInschrijving.AlleVakInschrijvingen)
                {
                    if (inschrijving.Student.Equals(this))
                    {
                        enkelVoorDezeStudent.Add(inschrijving);
                    }
                }
                return enkelVoorDezeStudent.ToImmutableList<VakInschrijving>();
            }
        }
        public ImmutableList<Cursus> Cursussen
        {
            get
            {
                var cursussen = new List<Cursus>();
                foreach (var inschrijving in this.VakInschrijvingen)
                {
                    cursussen.Add(inschrijving.Cursus);
                }
                return cursussen.ToImmutableList<Cursus>();
            }
        }
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
            if (cursusIndex < 0 || cursusIndex > this.VakInschrijvingen.Count)
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
                this.VakInschrijvingen[cursusIndex].Resultaat = behaaldCijfer;
            } 
        }
        public double Gemiddelde()
        {
            int aantalCursussen = 0;
            double som = 0.0;
            foreach (VakInschrijving vakInschrijving in VakInschrijvingen)
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
            VakInschrijvingen.Add(new VakInschrijving(this, cursus, resultaat));
        }
        public override double BepaalWerkbelasting()
        {
            double werkbelasting = 0.0;
            foreach (VakInschrijving vakinschrijving in VakInschrijvingen)
            {
                werkbelasting += 10;
            }
            return werkbelasting;
        }
        public override string GenereerNaamkaartje()
        {
            return $"{Naam} (STUDENT)";
        }
        public void RegistreerCursusResultaat(Cursus cursus, byte? behaaldResultaat)
        {
            new VakInschrijving(this, cursus, behaaldResultaat);
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
            foreach (VakInschrijving vakInschrijving in VakInschrijvingen)
            {
                if (vakInschrijving is not null)
                {
                    Console.WriteLine($"{vakInschrijving.Cursus.Titel}:\t{vakInschrijving.Resultaat}");
                }
            }
            Console.WriteLine($"Gemiddelde:\t{Gemiddelde():F1}\n");
        }
        public override string ToCSV()
        {
            string uitkomst = $"Student;{CSVPersoonsGegevens}";
            foreach (var item in Dossier)
            {
                uitkomst += $";{item.Key};{item.Value}";
            }
            return uitkomst;
        }
        public override string ToString()
        {
            return $"{base.ToString()}\nMeerbepaald, student";
        }
        public static void ToonStudenten()
        {
            Console.WriteLine("Toon studenten in:");
            Console.WriteLine("1. Stijgende alfabetische volgorde");
            Console.WriteLine("2. Dalende alfabetische volgorde");
            int keuze = Convert.ToInt32(Console.ReadLine());
            IComparer<Student> comparer = null;
            if (keuze == 1)
            {
                comparer = new StudentVolgensNaamOplopendComparer();
            }
            else if (keuze == 2)
            {
                comparer = new StudentVolgensNaamAflopendComparer();
            }
            else
            {
            }
            ImmutableList<Student> alleStudentenGesorteerd = AlleStudenten.Sort(comparer);
            foreach (Student student in alleStudentenGesorteerd)
            {
                Console.WriteLine(student.ToString());
            }
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
            Cursus communicatie = new Cursus("Communicatie");
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", 6);
            Cursus databanken = new Cursus("Databanken", 5);

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

            foreach (var student in AlleStudenten)
            {
                student.ToCSV();
            }
        }
        public static void DemonstreerStudentUitTekstFormaat()
        {
            Console.WriteLine("Geef de tekstvoorstelling van 1 student in CSV-formaat:");
            string csvWaarde = Console.ReadLine();
            Student student = Student.StudentUitTekstFormaat(csvWaarde);
            student.ToonOverzicht();
        }
        public static void LeesVanafCommandLine()
        {
            Console.WriteLine("Naam van de student?");
            var naam = Console.ReadLine();
            Console.WriteLine("Geboortedatum van de student?");
            var geboorteDatum = Convert.ToDateTime(Console.ReadLine());
            new Student(naam, geboorteDatum);
        }
    }
}
