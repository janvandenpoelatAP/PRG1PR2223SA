using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAdmin
{
    public class Lector:Personeel
    {
        private static List<Lector> alleLectoren = new List<Lector>();
        public static ImmutableList<Lector> AlleLectoren
        {
            get
            {
                return alleLectoren.ToImmutableList<Lector>();
            }
        }
        private Dictionary<Cursus, double> cursussen;
        public Lector(string naam, DateTime geboortedatum, Dictionary<string, byte> taken) : base(naam, geboortedatum, taken)
        {
            alleLectoren.Add(this);
            this.cursussen = new Dictionary<Cursus, double>();
        }
        public override double BepaalWerkbelasting()
        {
            double totaal = 0;
            foreach (double geroosterd in this.cursussen.Values)
            {
                totaal += geroosterd;
            }
            return totaal;
        }
        public override uint BerekenSalaris()
        {
            // per 4 jaar 120 euro extra bovenop basisloon 2200
            // maar tewerkstelling moet ook in rekening gebracht (max 40u)
            double basis = (2200 + Math.Min(40, (this.Ancienniteit / 4)) * 120);
            return (uint)(basis * Math.Min(40, this.BepaalWerkbelasting()) / 40);
        }
        public override string GenereerNaamkaartje()
        {
            var builder = new StringBuilder();
            builder.AppendLine(this.Naam);
            builder.AppendLine("Lector voor:");
            foreach (Cursus cursus in this.cursussen.Keys)
            {
                builder.AppendLine(cursus.Titel);
            }
            return builder.ToString();
        }
        public override string ToString()
        {
            return $"{base.ToString()}\nMeerbepaald, lector";
        }
        public static void DemonstreerLectoren()
        {
            var taken = new Dictionary<string, byte>();
            var economie = new Cursus("Economie");
            var statistiek = new Cursus("Statistiek");
            var analytischeMeetkunde = new Cursus("Analytische meetkunde");
            var anna = new Lector("Anna Bolzano", new DateTime(1975, 6, 12), taken);
            anna.cursussen.Add(economie, 3);
            anna.cursussen.Add(statistiek, 3);
            anna.cursussen.Add(analytischeMeetkunde, 4);
            anna.Ancienniteit = 9;
            foreach (var personeel in Personeel.AllePersoneel)
            {
                System.Console.WriteLine(personeel.GenereerNaamkaartje());
            }
            foreach (var personeel in Lector.AlleLectoren)
            {
                System.Console.WriteLine(personeel.GenereerNaamkaartje());
            }
            System.Console.WriteLine(anna.BerekenSalaris());
            System.Console.WriteLine(anna.BepaalWerkbelasting());
        }
    }
}

