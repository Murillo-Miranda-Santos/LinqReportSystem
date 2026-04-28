using LinqReportSystem.Models;

namespace LinqReportSystem;

public class Repository
{
    public List<Soldier> GetSoldiers()
    {
        return new List<Soldier>
        {
            new Soldier { Id = 1, Name = "John Carter" },
            new Soldier { Id = 2, Name = "Sarah Mitchell" },
            new Soldier { Id = 3, Name = "David Walker" },
            new Soldier { Id = 4, Name = "Emily Stone" },
            new Soldier { Id = 5, Name = "Michael Brooks" }
        };
    }

    public List<Training> GetTrainings()
    {
        return new List<Training>
        {
            new Training { Id = 1, Name = "Strength" },
            new Training { Id = 2, Name = "Endurance" },
            new Training { Id = 3, Name = "Agility" },
            new Training { Id = 4, Name = "Combat" }
        };
    }

    public List<Enrollment> GetEnrollments()
    {
        return new List<Enrollment>
        {
            new Enrollment { Id = 1, SoldierId = 1, TrainingId = 1, Score = 8 },
            new Enrollment { Id = 2, SoldierId = 1, TrainingId = 2, Score = 9 },
            new Enrollment { Id = 3, SoldierId = 1, TrainingId = 3, Score = 7 },

            new Enrollment { Id = 4, SoldierId = 2, TrainingId = 1, Score = 6 },
            new Enrollment { Id = 5, SoldierId = 2, TrainingId = 2, Score = 8 },
            new Enrollment { Id = 6, SoldierId = 2, TrainingId = 4, Score = 9 },

            new Enrollment { Id = 7, SoldierId = 3, TrainingId = 1, Score = 7 },
            new Enrollment { Id = 8, SoldierId = 3, TrainingId = 3, Score = 8 },
            new Enrollment { Id = 9, SoldierId = 4, TrainingId = 2, Score = 9 },

            new Enrollment { Id = 10, SoldierId = 4, TrainingId = 4, Score = 10 },
            new Enrollment { Id = 11, SoldierId = 5, TrainingId = 1, Score = 5 },
            new Enrollment { Id = 12, SoldierId = 5, TrainingId = 3, Score = 6 },
            new Enrollment { Id = 13, SoldierId = 5, TrainingId = 4, Score = 7 }
        };
    }
}
