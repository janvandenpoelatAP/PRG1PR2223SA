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
            this.student = student;
            this.cursus = cursus;
            this.Resultaat = resultaat;
            alleVakInschrijvingen.Add(this);
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
                for (int i = 1; i <= Student.AlleStudenten.Count; i++)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(Student.AlleStudenten[i - 1]);
                }
                Student gekozenStudent = Student.AlleStudenten[Convert.ToInt32(Console.ReadLine()) - 1];
                Console.WriteLine("Kies een cursus");
                for (int i = 1; i <= Cursus.AlleCursussen.Count; i++)
                {
                    Console.WriteLine(i);
                    Console.WriteLine(Cursus.AlleCursussen[i - 1].Titel);
                }
                Cursus gekozenCursus = Cursus.AlleCursussen[Convert.ToInt32(Console.ReadLine()) - 1];
                Console.WriteLine("Wil je een resultaat toekennen?");
                byte? resultaat = null;
                if (Console.ReadLine().ToLower().Trim() == "ja")
                {
                    Console.WriteLine("Wat is het resultaat?");
                    resultaat = Convert.ToByte(Console.ReadLine());
                }
                new VakInschrijving(gekozenStudent, gekozenCursus, resultaat);
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
