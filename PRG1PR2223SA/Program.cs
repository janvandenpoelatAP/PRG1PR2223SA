namespace PRG1PR2223SA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int keuze = 0;
            Console.WriteLine($"Wat wil je demonstreren?\n\t1. Studenten\n\t2. Cursussen\n");
            keuze = Convert.ToInt32(Console.ReadLine());
            if (keuze == 1)
            {
                Student.DemonstreerStudenten();
            }
            else if (keuze == 2)
            {
                Cursus.DemonstreerCursussen();
            }
        }
    }
}