using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Service.DataHolders
{
    public class ScoreBoardData
    {
        public List<Problem> Problems { get; set; }

        public List<TeamsScores> TeamsScores { get; set; }

        public ScoreBoardData(List<Problem> problems, List<TeamsScores> teamsScores)
        {
            Problems = problems;
            TeamsScores = teamsScores;
        }
    }
}
