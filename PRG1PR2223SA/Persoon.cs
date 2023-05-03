using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public abstract class Persoon
    {
        private static List<Persoon> allePersonen = new List<Persoon>();
        public static ImmutableList<Persoon> AllePersonen
        {
            get
            {
                return ImmutableList.ToImmutableList<Persoon>(Persoon.allePersonen);
            }
        }
        private static uint maxId = 1;

        private uint id;
        public uint Id
        {
            get
            {
                return id;
            }
        }
        private DateTime geboortedatum;
        public DateTime Geboortedatum
        {
            get
            {
                return this.geboortedatum;
            }
        }
        private string naam;
        public string Naam
        {
            get { return naam; }
            set { naam = value; }
        }

        public Persoon(string naam, DateTime geboortedatum)
        {
            this.naam = naam;
            this.geboortedatum = geboortedatum;
            this.id = Persoon.maxId;
            Persoon.maxId++;
            Persoon.allePersonen.Add(this);
        }
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is Persoon)
            {
                var objPersoon = (Persoon)obj;
                return objPersoon.Id == this.Id;
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
        public abstract double BepaalWerkbelasting();
        public abstract string GenereerNaamkaartje();
    }
}
