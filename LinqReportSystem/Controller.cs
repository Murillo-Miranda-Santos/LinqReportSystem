namespace LinqReportSystem;

public class Controller
{
    private readonly Service service = new Service();

    public void Run()
    {
        while (true)
        {
            int choice = Menu();
            Console.Clear();

            switch (choice)
            {
                case 1:
                    SoldierReport();
                    break;
                case 2:
                    TrainingReport();
                    break;
                case 3:
                    GeneralReport();
                    break;
                case 4:
                    Console.WriteLine("Exiting...");
                    return;
            }
        }
    }

    private int Menu()
    {
        Console.WriteLine("=========== MENU ============");
        Console.WriteLine("[1] Soldier Report");
        Console.WriteLine("[2] Training Report");
        Console.WriteLine("[3] General Report");
        Console.WriteLine("[4] Exit");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
        {
            Console.WriteLine("Invalid option.");
        }

        return choice;
    }

    private void SoldierReport()
    {
        var result = service.GetSoldierReport();

        foreach (var s in result)
        {
            Console.WriteLine($"Soldier: {s.Name}");
            Console.WriteLine($"Trainings: {s.TrainingCount}");
            Console.WriteLine($"Average Score: {s.AverageScore:F1}");
            Console.WriteLine($"Best Training: {s.BestTraining.Name}");
            Console.WriteLine("Trainings:");

            foreach (var t in s.Trainings)
            {
                Console.WriteLine($"- {t.Name} ({t.Score})");
            }

            Console.WriteLine();
        }
    }

    private void TrainingReport()
    {
        var result = service.GetTrainingReport();

        foreach (var t in result)
        {
            Console.WriteLine($"Training: {t.Name}");
            Console.WriteLine($"Participants: {t.ParticipantCount}");
            Console.WriteLine($"Average Score: {t.AverageScore:F1}");
            Console.WriteLine($"Best Soldier: {t.BestSoldier.Name}");
            Console.WriteLine("Participants:");

            foreach (var s in t.Soldiers)
            {
                Console.WriteLine($"- {s.Name} ({s.Score})");
            }

            Console.WriteLine();
        }
    }

    private void GeneralReport()
    {
        var result = service.GetGeneralReport();

        Console.WriteLine($"Total Enrollments: {result.TotalEnrollments}");
        Console.WriteLine($"Average Score: {result.AverageScore:F1}");
        Console.WriteLine($"Best Soldier: {result.BestSoldier.Name}");
    }
}
