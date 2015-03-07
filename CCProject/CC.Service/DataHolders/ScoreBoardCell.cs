using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CC.Service.DataHolders
{
    public class ScoreBoardCell
    {
        public int SolutionId { get; set; }

        public int Score { get; set; }

        public ScoreBoardCell(int solutionId, int score)
        {
            SolutionId = solutionId;
            Score = score;
        }
    }
}
