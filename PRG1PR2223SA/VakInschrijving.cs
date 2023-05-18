using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class VakInschrijving
    {
        private static List<VakInschrijving> alleVakInschrijvingen = new List<VakInschrijving>();
        public static ImmutableList<VakInschrijving> AlleVakInschrijvingen
        {
            get
            {
                return alleVakInschrijvingen.ToImmutableList<VakInschrijving>();
            }
        }
        private Student student;
        public Student Student
        {
            get
            {
                return student;
            }
        }
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
        public VakInschrijving(Student student, Cursus cursus, byte? resultaat)
        {
            if (student is null || cursus is null)
            {
                throw new ArgumentException("Student of cursus mag niet null zijn");
            }
            if (ZoekVakInschrijvingOpCursusEnStudent(student, cursus) is not null)
            {
                throw new ArgumentException($"Student {student.Naam} is al ingeschreven voor cursus {cursus.Titel}");
            }
            this.student = student;
            this.cursus = cursus;
            this.Resultaat = resultaat;
            alleVakInschrijvingen.Add(this);
        }
        public static VakInschrijving ZoekVakInschrijvingOpCursusEnStudent(Student student, Cursus cursus)
        {
            foreach (VakInschrijving vakInschrijving in VakInschrijving.AlleVakInschrijvingen)
            {
                if (vakInschrijving.Cursus == cursus && vakInschrijving.Student == student)
                {
                    return vakInschrijving;
                }
            }
            return null;
        }
        public static void LeesVanafCommandLine()
        {
            // het zou efficiënter zijn AlleStudenten bij te houden in een variabele
            // komt omdat we dit telkens berekenen
            if (Student.AlleStudenten.Count < 1 || Cursus.AlleCursussen.Count < 1)
            {
                Console.WriteLine("Er moet minstens één student zijn en minstens één cursus");
            }
            else
            {
                Console.WriteLine("Kies een student");
                Console.WriteLine(0);
                Console.WriteLine("null");
                for (int i = 1; i <= Student.AlleStudenten.Count; i++)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(Student.AlleStudenten[i - 1]);
                }
                Student gekozenStudent = null;
                int keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze != 0)
                {
                    gekozenStudent = Student.AlleStudenten[keuze - 1];
                }
                Console.WriteLine("Kies een cursus");
                Console.WriteLine(0);
                Console.WriteLine("null");
                for (int i = 1; i <= Cursus.AlleCursussen.Count; i++)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(Cursus.AlleCursussen[i - 1].Titel);
                }
                Cursus gekozenCursus = null;
                keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze != 0)
                {
                    gekozenCursus = Cursus.AlleCursussen[keuze - 1];
                }
                Console.WriteLine("Wil je een resultaat toekennen?");
                byte? resultaat = null;
                if (Console.ReadLine().ToLower().Trim() == "ja")
                {
                    Console.WriteLine("Wat is het resultaat?");
                    resultaat = Convert.ToByte(Console.ReadLine());
                }
                try
                {
                    new VakInschrijving(gekozenStudent, gekozenCursus, resultaat);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void ToonInschrijvingsGegevens()
        {
            foreach (var item in VakInschrijving.AlleVakInschrijvingen)
            {
                Console.WriteLine($"{item.Student}\ningeschreven voor\n{item.Cursus.Titel}");
            }
        }
    }
}
