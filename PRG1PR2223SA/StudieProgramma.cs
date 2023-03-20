using SchoolAdmin;
using System;
using System.Collections.Generic;
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
        private Cursus[] cursussen = new Cursus[20];
        public Cursus[] Cursussen
        {
            get
            {
                return cursussen;
            }
            set
            {
                if (value is null)
                {
                    Console.WriteLine("Null is niet toegelaten");
                }
                else
                {
                    cursussen = value;
                }
            }
        }
        public StudieProgramma(string naam)
        {
            this.naam = naam;
        }
        public void ToonOverzicht()
        {
            Console.WriteLine($"{Naam}:");
            for (int i = 0; i < Cursussen.Length; i++)
            {
                if (Cursussen[i] is not null)
                {
                    Cursussen[i].ToonOverzicht();
                }
            }
        }
        public static void DemonstreerStudieProgrmma()
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
            Cursus databanken = new Cursus("Databanken", new Student[7], 5);
            Cursus[] cursussen1 = { communicatie, programmeren, databanken };
            Cursus[] cursussen2 = { communicatie, programmeren, databanken };
            StudieProgramma programmerenProgramma = new StudieProgramma("Programmeren");
            StudieProgramma snbProgramma = new StudieProgramma("Systeem- en netwerkbeheer");
            programmerenProgramma.cursussen = cursussen1;
            snbProgramma.cursussen = cursussen2;
            // later wordt Databanken geschrapt uit het programma SNB
            // voor SNB wordt bovendien Programmeren hernoemd naar Scripting
            snbProgramma.cursussen[2] = null;
            //snbProgramma.cursussen[1].Titel = "Scripting";
            Cursus scripting = new Cursus("Scripting");
            snbProgramma.cursussen[1] = scripting; ;
            programmerenProgramma.ToonOverzicht();
            snbProgramma.ToonOverzicht();
        }
    }
}
