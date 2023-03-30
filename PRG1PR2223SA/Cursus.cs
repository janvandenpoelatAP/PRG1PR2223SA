using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class Cursus
    {
        private static List<Cursus> alleCursussen = new List<Cursus>();
        public static ImmutableList<Cursus> AlleCursussen
        {
            get
            {
                return alleCursussen.ToImmutableList();
            }
        }
        private static int maxId = 1;
        private int id;
        public int Id
        {
            get
            {
                return id;
            }
        }
        public string Titel;
        public List<Student> Studenten = new List<Student>();
        private byte studiepunten;
        public byte Studiepunten
        {
            get
            {
                return studiepunten;
            }
            private set
            {
                studiepunten = value;
            }
        }
        public Cursus(string titel, List<Student> studenten, byte studiepunten)
        {
            this.Titel = titel;
            this.Studenten = studenten;
            this.Studiepunten = studiepunten;
            this.id = Cursus.maxId;
            RegistreerCursus(this);
            Cursus.maxId++;
        }
        public Cursus(string titel, List<Student> studenten) : this(titel, studenten, 3)
        {
        }
        public Cursus(string titel) : this(titel, new List<Student>())
        {
        }
        public void ToonOverzicht()
        {
            Console.WriteLine($"{Titel} ({Id} - {Studiepunten} stp)");
            foreach (Student student in Studenten)
            {
                if (student is not null)
                {
                    Console.WriteLine($"{student.Naam}");
                }
            }
            Console.WriteLine();
        }
        public static void RegistreerCursus(Cursus cursus)
        {
            alleCursussen.Add(cursus);
        }
        public static Cursus ZoekCursusOpId(int id)
        {
            foreach (Cursus cursus in AlleCursussen)
            {
                if (cursus.Id == id)
                {
                    return cursus;
                }
            }
            return null;
        }
        public static void DemonstreerCursussen()
        {
            Cursus communicatie = new Cursus("Communicatie", new List<Student>());
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", new List<Student>(), 6);
            Cursus databanken = new Cursus("Databanken", new List<Student>(), 5);

            Student student1 = new Student("Said Aziz", new DateTime(2001, 1, 3));
            Student student2 = new Student("Mieke Vermeulen", new DateTime(2000, 2, 1));

            communicatie.Studenten.Add(student1);
            communicatie.Studenten.Add(student2);
            programmeren.Studenten.Add(student1);
            programmeren.Studenten.Add(student2);
            webtechnologie.Studenten.Add(student1);
            databanken.Studenten.Add(student2);

            communicatie.ToonOverzicht();
            programmeren.ToonOverzicht();
            webtechnologie.ToonOverzicht();
            databanken.ToonOverzicht();

            Cursus cursus = ZoekCursusOpId(3);
            cursus.ToonOverzicht();
        }
    }
}
