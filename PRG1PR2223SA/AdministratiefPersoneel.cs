using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class AdministratiefPersoneel:Personeel
    {
        public static ImmutableList<AdministratiefPersoneel> AlleAdministratiefPersoneel
        {
            get
            {
                var enkelAdministratiefPersoneel = new List<AdministratiefPersoneel>();
                foreach (var persoon in Persoon.AllePersonen)
                {
                    if (persoon is AdministratiefPersoneel)
                    {
                        enkelAdministratiefPersoneel.Add((AdministratiefPersoneel)persoon);
                    }
                }
                return enkelAdministratiefPersoneel.ToImmutableList<AdministratiefPersoneel>();
            }
        }
        public AdministratiefPersoneel(string naam, DateTime geboortedatum, Dictionary<string, byte> taken) : base(naam, geboortedatum, taken)
        {
        }
        public override uint BerekenSalaris()
        {
            // per 3 jaar 75 euro extra bovenop basisloon 2000
            // maar tewerkstelling moet ook in rekening gebracht (max 40u)
            double basis = (2000 + Math.Min(40, (this.Ancienniteit / 3)) * 75);
            return (uint)(basis * Math.Min(40, this.BepaalWerkbelasting()) / 40);
        }
        public override double BepaalWerkbelasting()
        {
            double totaal = 0;
            foreach (byte taakLengte in this.Taken.Values)
            {
                totaal += taakLengte;
            }
            return totaal;
        }
        public override string GenereerNaamkaartje()
        {
            return $@"{this.Naam} (ADMINISTRATIE)";
        }
        public override string ToString()
        {
            return $"{base.ToString()}\nMeerbepaald, administratief personeel";
        }
        public static void DemonstreerAdministratiefPersoneel()
        {
            var taken = new Dictionary<string, byte>();
            taken.Add("roostering", 10);
            taken.Add("correspondentie", 10);
            taken.Add("animatie", 10);
            AdministratiefPersoneel ahmed = new AdministratiefPersoneel("Ahmed Azzaoui", new DateTime(1988, 2, 4), taken);
            ahmed.Ancienniteit = 4;
            foreach (var personeel in AlleAdministratiefPersoneel)
            {
                System.Console.WriteLine(personeel.GenereerNaamkaartje());
            }
            foreach (var personeel in AllePersoneel)
            {
                System.Console.WriteLine(personeel.GenereerNaamkaartje());
            }
            System.Console.WriteLine(ahmed.BerekenSalaris());
            System.Console.WriteLine(ahmed.BepaalWerkbelasting());
        }
    }
}
