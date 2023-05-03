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
    }
}
