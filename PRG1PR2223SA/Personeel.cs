using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public abstract class Personeel:Persoon
    {
        public static ImmutableList<Personeel> AllePersoneel
        {
            get
            {
                var enkelPersoneel = new List<Personeel>();
                foreach (var persoon in Persoon.AllePersonen)
                {
                    if (persoon is Personeel)
                    {
                        enkelPersoneel.Add((Personeel)persoon);
                    }
                }
                return enkelPersoneel.ToImmutableList<Personeel>();
            }
        }
        private byte ancienniteit;
        public byte Ancienniteit
        {
            get
            {
                return this.ancienniteit;
            }
            set
            {
                byte maximum = 50;
                this.ancienniteit = Math.Min(maximum, value);
            }
        }
        private Dictionary<string, byte> taken;
        public ImmutableDictionary<string, byte> Taken
        {
            get
            {
                var builder = ImmutableDictionary.CreateBuilder<string, byte>();
                foreach (KeyValuePair<string, byte> pair in taken)
                {
                    builder.Add(pair);
                }
                return builder.ToImmutableDictionary<string, byte>();
            }
        }
        public Personeel(string naam, DateTime geboortedatum, Dictionary<string, byte> taken):base(naam, geboortedatum)
        {
            this.taken = new Dictionary<string, byte>();
            if (!(taken is null))
            {
                foreach (var paar in taken)
                {
                    this.taken.Add(paar.Key, paar.Value);
                }
            }
        }
        public abstract uint BerekenSalaris();
    }
}

