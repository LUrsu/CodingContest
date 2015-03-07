using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Service.DataHolders
{
    public class TeamsScores
    {
        public List<ScoreBoardCell> ProblemScoreData { get; set; }

        public int TotalScore { get; set; }

        public int TeamId { get; set; }

        public string TeamName { get; set; }

        public TeamsScores(List<ScoreBoardCell> problemScoreData, Team team)
        {
            ProblemScoreData = problemScoreData;
            TotalScore = ProblemScoreData.Select(s => s.Score).Sum();
            TeamId = team.Id;
            TeamName = team.Name;
        }
    }
}
