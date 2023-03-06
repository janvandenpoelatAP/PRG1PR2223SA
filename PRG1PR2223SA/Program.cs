namespace PRG1PR2223SA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student();
            student1.Naam = "Said Aziz";
            student1.GeboorteDatum = new DateTime(2000, 6, 1);
            student1.RegistreerVoorCursus("Programmeren");
            student1.RegistreerVoorCursus("DataBanken");
            Student student2 = new Student();
            student2.Naam = "Mieke Vermeulen";
            student2.GeboorteDatum = new DateTime(1998, 1, 1);
            student2.RegistreerVoorCursus("Programmeren");
            Console.WriteLine($"{student1.GenereerNaamkaarje()} => werkbelasting: {student1.BepaalWerkbelasting()}");
            Console.WriteLine($"{student2.GenereerNaamkaarje()} => werkbelasting: {student2.BepaalWerkbelasting()}");
        }
    }
}