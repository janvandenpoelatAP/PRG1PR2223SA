using SchoolAdmin;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    internal class StudieProgramma
    {
        private string naam;
        public string Naam
        {
            get
            {
                return naam;
            }
        }
        private Dictionary<Cursus, byte> cursussen = new Dictionary<Cursus, byte>();
        public ImmutableList<Cursus> Cursussen
        {
            get
            {
                return this.cursussen.Keys.ToImmutableList<Cursus>();
            }
        }
        public StudieProgramma(string naam)
        {
            this.naam = naam;
        }
        public void ToonOverzicht()
        {
            Console.WriteLine($"{Naam}:");
            foreach (Cursus cursus in Cursussen)
            {
                if (cursus is not null)
                {
                    cursus.ToonOverzicht();
                }
            }
        }
        public static void DemonstreerStudieProgramma()
        {
            /* Stap 1
            Cursus communicatie = new Cursus("Communicatie");
            Cursus programmeren = new Cursus("Programmeren");
            Cursus databanken = new Cursus("Databanken", new Student[7], 5);
            //Cursus[] cursussen = { communicatie, programmeren, databanken };
            //StudieProgramma programmerenProgramma = new StudieProgramma("Programmeren");
            //StudieProgramma snbProgramma = new StudieProgramma("Systeem- en netwerkbeheer");
            //programmerenProgramma.cursussen = cursussen;
            //snbProgramma.cursussen = cursussen;
            Cursus[] cursussenProgrammeren = { communicatie, programmeren, databanken };
            Cursus[] cursussenSnb = { communicatie, programmeren, databanken };
            StudieProgramma programmerenProgramma = new StudieProgramma("Programmeren");
            StudieProgramma snbProgramma = new StudieProgramma("Systeem- en netwerkbeheer");
            programmerenProgramma.cursussen = cursussenProgrammeren;
            snbProgramma.cursussen = cursussenSnb;
            // later wordt Databanken geschrapt uit het programma SNB
            snbProgramma.cursussen[2] = null;
            programmerenProgramma.ToonOverzicht();
            snbProgramma.ToonOverzicht();
            */
            Cursus communicatie = new Cursus("Communicatie");
            Cursus programmeren = new Cursus("Programmeren");
            Cursus databanken = new Cursus("Databanken", 5);
            Cursus scripting = new Cursus("Scripting");
            var cursussen1Dictionary = new Dictionary<Cursus, byte>();
            cursussen1Dictionary.Add(communicatie, 1);
            cursussen1Dictionary.Add(programmeren, 1);
            cursussen1Dictionary.Add(databanken, 1);
            // gewoon een andere schrijfwijze voor dictionaries
            var cursussen2Dictionary = new Dictionary<Cursus, byte> { { communicatie, 2 }, { scripting, 1 }, { databanken, 1 } };
            StudieProgramma programmerenProgramma = new StudieProgramma("Programmeren");
            StudieProgramma snbProgramma = new StudieProgramma("Systeem- en netwerkbeheer");
            programmerenProgramma.cursussen = cursussen1Dictionary;
            snbProgramma.cursussen = cursussen2Dictionary;
            // later wordt Databanken geschrapt uit het programma SNB
            snbProgramma.cursussen.Remove(databanken);
            programmerenProgramma.ToonOverzicht();
            snbProgramma.ToonOverzicht();
        }
    }
}
