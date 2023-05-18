namespace SchoolAdmin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Wat wil je doen?");
                Console.WriteLine("1. DemonstreerStudenten uitvoeren");
                Console.WriteLine("2. DemonstreerCursussen uitvoeren");
                Console.WriteLine("3. DemonstreerStudentUitTekstFormaat uitvoeren");
                Console.WriteLine("4. DemonstreerStudieProgramma uitvoeren");
                Console.WriteLine("5. DemonstreerAdministratiefPersoneel uitvoeren");
                Console.WriteLine("6. DemonstreerLectoren uitvoeren");
                Console.WriteLine("7. Student toevoegen");
                Console.WriteLine("8. Cursus toevoegen");
                Console.WriteLine("9. VakInschrijving toevoegen");
                Console.WriteLine("10. Inschrijvingsgegevens tonen");
                Console.WriteLine("11. Studenten tonen");
                Console.WriteLine("12. Cursussen tonen");
                int antwoord = Convert.ToInt32(Console.ReadLine());
                switch (antwoord)
                {
                    case 1:
                        Student.DemonstreerStudenten();
                        break;
                    case 2:
                        Cursus.DemonstreerCursussen();
                        break;
                    case 3:
                        Student.DemonstreerStudentUitTekstFormaat();
                        break;
                    case 4:
                        StudieProgramma.DemonstreerStudieProgramma();
                        break;
                    case 5:
                        AdministratiefPersoneel.DemonstreerAdministratiefPersoneel();
                        break;
                    case 6:
                        Lector.DemonstreerLectoren();
                        break;
                    case 7:
                        Student.LeesVanafCommandLine();
                        break;
                    case 8:
                        Cursus.LeesVanafCommandLine();
                        break;
                    case 9:
                        VakInschrijving.LeesVanafCommandLine();
                        break;
                    case 10:
                        VakInschrijving.ToonInschrijvingsGegevens();
                        break;
                    case 11:
                        Student.ToonStudenten();
                        break;
                    case 12:
                        Cursus.ToonCursussen();
                        break;
                    default:
                        Console.WriteLine("Ongeldig antwoord.");
                        break;
                }
            }
        }
    }
}
