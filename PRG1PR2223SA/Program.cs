namespace PRG1PR2223SA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student();
            student1.Naam = "Said Aziz";
            student1.GeboorteDatum = new DateTime(2000, 6, 1);
            student1.Cursussen = new string[5];
            student1.Cursussen[0] = "Programmeren";
            student1.Cursussen[1] = "DataBanken";
            Student student2 = new Student();
            student2.Naam = "Mieke Vermeulen";
            student2.GeboorteDatum = new DateTime(1998, 1, 1);
            student2.Cursussen = new string[5];
            student2.Cursussen[0] = "Programmeren";
            Console.WriteLine($"{student1.GenereerNaamkaarje()} => werkbelasting: {student1.BepaalWerkbelasting()}");
            Console.WriteLine($"{student2.GenereerNaamkaarje()} => werkbelasting: {student2.BepaalWerkbelasting()}");
        }
    }
}