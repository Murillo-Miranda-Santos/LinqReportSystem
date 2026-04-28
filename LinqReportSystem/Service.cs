using LinqReportSystem.Models;

namespace LinqReportSystem;

public class Service
{
    private readonly Repository Repository = new Repository();

    // DTOs
    public class SoldierReport
    {
        public string Name { get; set; }
        public int TrainingCount { get; set; }
        public decimal AverageScore { get; set; }
        public Training BestTraining { get; set; }
        public List<TrainingScore> Trainings { get; set; }
    }

    public class TrainingScore
    {
        public string Name { get; set; }
        public decimal Score { get; set; }
    }

    public class TrainingReport
    {
        public string Name { get; set; }
        public int ParticipantCount { get; set; }
        public decimal AverageScore { get; set; }
        public Soldier BestSoldier { get; set; }
        public List<SoldierScore> Soldiers { get; set; }
    }

    public class SoldierScore
    {
        public string Name { get; set; }
        public decimal Score { get; set; }
    }

    public class GeneralReport
    {
        public int TotalEnrollments { get; set; }
        public decimal AverageScore { get; set; }
        public Soldier BestSoldier { get; set; }
    }

    // Soldier Report
    public List<SoldierReport> GetSoldierReport()
    {
        var soldiers = Repository.GetSoldiers();
        var trainings = Repository.GetTrainings();
        var enrollments = Repository.GetEnrollments();

        var result = enrollments
            .Join(soldiers, e => e.SoldierId, s => s.Id, (e, s) => new { e, s })
            .Join(trainings, x => x.e.TrainingId, t => t.Id, (x, t) => new
            {
                SoldierId = x.s.Id,
                SoldierName = x.s.Name,
                TrainingId = t.Id,
                TrainingName = t.Name,
                Score = x.e.Score
            })
            .GroupBy(x => x.SoldierId)
            .Select(g =>
            {
                var best = g.OrderByDescending(x => x.Score).First();

                return new SoldierReport
                {
                    Name = g.First().SoldierName,
                    TrainingCount = g.Count(),
                    AverageScore = g.Average(x => x.Score),
                    BestTraining = new Training
                    {
                        Id = best.TrainingId,
                        Name = best.TrainingName
                    },
                    Trainings = g.Select(x => new TrainingScore
                    {
                        Name = x.TrainingName,
                        Score = x.Score
                    }).ToList()
                };
            })
            .ToList();

        return result;
    }

    // Training Report
    public List<TrainingReport> GetTrainingReport()
    {
        var soldiers = Repository.GetSoldiers();
        var trainings = Repository.GetTrainings();
        var enrollments = Repository.GetEnrollments();

        var result = enrollments
            .Join(trainings, e => e.TrainingId, t => t.Id, (e, t) => new { e, t })
            .Join(soldiers, x => x.e.SoldierId, s => s.Id, (x, s) => new
            {
                TrainingId = x.t.Id,
                TrainingName = x.t.Name,
                SoldierId = s.Id,
                SoldierName = s.Name,
                Score = x.e.Score
            })
            .GroupBy(x => x.TrainingId)
            .Select(g =>
            {
                var best = g.OrderByDescending(x => x.Score).First();

                return new TrainingReport
                {
                    Name = g.First().TrainingName,
                    ParticipantCount = g.Count(),
                    AverageScore = g.Average(x => x.Score),
                    BestSoldier = new Soldier
                    {
                        Id = best.SoldierId,
                        Name = best.SoldierName
                    },
                    Soldiers = g.Select(x => new SoldierScore
                    {
                        Name = x.SoldierName,
                        Score = x.Score
                    }).ToList()
                };
            })
            .ToList();

        return result;
    }

    // General Report
    public GeneralReport GetGeneralReport()
    {
        var soldiers = Repository.GetSoldiers();
        var enrollments = Repository.GetEnrollments();

        var grouped = enrollments
            .Join(soldiers, e => e.SoldierId, s => s.Id, (e, s) => new
            {
                SoldierId = s.Id,
                SoldierName = s.Name,
                Score = e.Score
            })
            .GroupBy(x => x.SoldierId)
            .OrderByDescending(g => g.Average(x => x.Score))
            .First();

        return new GeneralReport
        {
            TotalEnrollments = enrollments.Count(),
            AverageScore = enrollments.Average(x => x.Score),
            BestSoldier = new Soldier
            {
                Id = grouped.First().SoldierId,
                Name = grouped.First().SoldierName
            }
        };
    }
}
