using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class Cursus
    {
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
        public Student[] Studenten;
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
        public Cursus(string titel, Student[] studenten, byte studiepunten)
        {
            this.Titel = titel;
            this.Studenten = studenten;
            this.Studiepunten = studiepunten;
            this.id = Cursus.maxId;
            Cursus.maxId++;
        }
        public Cursus(string titel, Student[] studenten) : this(titel, studenten, 3)
        {
        }
        public Cursus(string titel) : this(titel, new Student[2])
        {
        }
        public void ToonOverzicht()
        {
            Console.WriteLine($"{Titel} ({Id} - {Studiepunten} stp)");
            for (int i = 0; i < Studenten.Length; i++)
            {
                if (Studenten[i] is not null)
                {
                    Console.WriteLine($"{Studenten[i].Naam}");
                }
            }
            Console.WriteLine();
        }
        public static void DemonstreerCursussen()
        {
            Cursus communicatie = new Cursus("Communicatie", new Student[2]);
            Cursus programmeren = new Cursus("Programmeren");
            Cursus webtechnologie = new Cursus("Webtechnologie", new Student[5], 6);
            Cursus databanken = new Cursus("Databanken", new Student[7], 5);

            Student student1 = new Student();
            student1.Geboortedatum = new DateTime(2001, 1, 3);
            student1.Naam = "Said Aziz";


            Student student2 = new Student();
            student2.Geboortedatum = new DateTime(2000, 2, 1);
            student2.Naam = "Mieke Vermeulen";

            communicatie.Studenten[0] = student1;
            communicatie.Studenten[1] = student2;
            programmeren.Studenten[0] = student1;
            programmeren.Studenten[1] = student2;
            webtechnologie.Studenten[0] = student1;
            databanken.Studenten[0] = student2;

            communicatie.ToonOverzicht();
            programmeren.ToonOverzicht();
            webtechnologie.ToonOverzicht();
            databanken.ToonOverzicht();
        }
    }
}
