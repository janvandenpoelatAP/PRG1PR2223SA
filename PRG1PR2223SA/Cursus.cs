using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Cursus : ICSVSerializable
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
            try
            {
                this.Titel = titel;
                this.id = Cursus.maxId;
                this.Studiepunten = studiepunten;
                RegistreerCursus(this);
                Cursus.maxId++;
            }
            catch (DuplicateDataException ex)
            {
                Console.WriteLine($"{ex.Message} {((Cursus)ex.Waarde2).Id}");
            }
        }
        public Cursus(string titel) : this(titel, 3)
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
        public string ToCSV()
        {
            return $"Cursus;{this.Id};\"{this.Titel}\";{this.Studiepunten}";
        }
        public override string ToString()
        {
            return $"Cursus {this.Titel} heeft {this.Studiepunten} studiepunten";
        }
        public static void RegistreerCursus(Cursus cursus)
        {
            Cursus cursusBestaand = ZoekCursusOpTitel(cursus.Titel);
            if (cursusBestaand is not null)
            {
                throw new DuplicateDataException("Nieuwe cursus heeft dezelfde naam als een bestaande cursus: ", cursus, cursusBestaand);
            }
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
        public static Cursus ZoekCursusOpTitel(string titel)
        {
            foreach (Cursus cursus in AlleCursussen)
            {
                if (cursus.Titel == titel)
                {
                    return cursus;
                }
            }
            return null;
        }
        public static void ToonCursussen()
        {
            Console.WriteLine("Toon cursussen in:");
            Console.WriteLine("1. Stijgende alfabetische volgorde");
            Console.WriteLine("2. Stijgende volgorde van studiepunten");
            int keuze = Convert.ToInt32(Console.ReadLine());
            IComparer<Cursus> comparer = null;
            if (keuze == 1)
            {
                comparer = new CursusVolgensTitelOplopendComparer();
            }
            else if (keuze == 2)
            {
                comparer = new CursusVolgensStudiepuntenOplopendComparer();
            }
            else
            {
            }
            ImmutableList<Cursus> alleCursussenGesorteerd = AlleCursussen.Sort(comparer);
            foreach (Cursus cursus in alleCursussenGesorteerd)
            {
                Console.WriteLine(cursus.ToString());
            }
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
        public static void LeesVanafCommandLine()
        {
            Console.WriteLine("Titel van de cursus?");
            var titel = Console.ReadLine();
            Console.WriteLine("Aantal studiepunten?");
            byte aantal = Convert.ToByte(Console.ReadLine());
            new Cursus(titel, aantal);
        }
    }
}
