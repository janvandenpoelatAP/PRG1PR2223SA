using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Cursus
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
        public ImmutableList<VakInschrijving> VakInschrijvingen
        {
            get
            {
                var enkelVoorDezeCursus = new List<VakInschrijving>();
                foreach (var inschrijving in VakInschrijving.AlleVakInschrijvingen)
                {
                    if (inschrijving.Cursus.Equals(this))
                    {
                        enkelVoorDezeCursus.Add(inschrijving);
                    }
                }
                return enkelVoorDezeCursus.ToImmutableList<VakInschrijving>();
            }
        }
        public ImmutableList<Student> Studenten
        {
            get
            {
                var studenten = new List<Student>();
                foreach (var inschrijving in this.VakInschrijvingen)
                {
                    studenten.Add(inschrijving.Student);
                }
                return studenten.ToImmutableList<Student>();
            }
        }
        public Cursus(string titel, byte studiepunten)
        {
            this.Titel = titel;
            this.Studiepunten = studiepunten;
            this.id = Cursus.maxId;
            RegistreerCursus(this);
            Cursus.maxId++;
        }
        public Cursus(string titel, List<Student> studenten) : this(titel, 3)
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
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is Cursus)
            {
                var objCursus = (Cursus)obj;
                return objCursus.Id == this.Id;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
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
            Cursus communicatie = new Cursus("Communicatie");
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", 6);
            Cursus databanken = new Cursus("Databanken", 5);

            Student student1 = new Student("Said Aziz", new DateTime(2001, 1, 3));
            student1.RegistreerCursusResultaat(communicatie, 12);
            student1.RegistreerCursusResultaat(programmeren, null);
            student1.RegistreerCursusResultaat(webtechnologie, 13);
            student1.ToonOverzicht();

            Student student2 = new Student("Mieke Vermeulen", new DateTime(2000, 2, 1));
            student2.RegistreerCursusResultaat(communicatie, 13);
            student2.RegistreerCursusResultaat(programmeren, null);
            student2.RegistreerCursusResultaat(databanken, 14);

            communicatie.ToonOverzicht();
            programmeren.ToonOverzicht();
            webtechnologie.ToonOverzicht();
            databanken.ToonOverzicht();

            Cursus cursus = ZoekCursusOpId(3);
            cursus.ToonOverzicht();
        }
    }
}
