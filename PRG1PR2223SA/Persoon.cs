using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public abstract class Persoon : ICSVSerializable
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
        public byte Leeftijd
        {
            get
            {
                var nu = DateTime.Now;
                var verschilJaren = nu.Year - this.Geboortedatum.Year;
                if (nu.Month < this.Geboortedatum.Month || nu.Month == this.Geboortedatum.Month && nu.Day < this.Geboortedatum.Day)
                {
                    verschilJaren--;
                }
                return Convert.ToByte(verschilJaren);
            }
        }
        public virtual string CSVPersoonsGegevens
        {
            get
            {
                return $"{Id};\"{Naam}\";{Geboortedatum.ToString(new CultureInfo("nl-BE"))}";
            }
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
        public override string ToString()
        {
            return @$"Persoon
-------
Naam: {this.Naam}
Leeftijd: {this.Leeftijd}";
        }
        public abstract double BepaalWerkbelasting();
        public abstract string GenereerNaamkaartje();
        public abstract string ToCSV();
    }
}
