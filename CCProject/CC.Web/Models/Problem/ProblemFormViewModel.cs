using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Problem
{
    public class ProblemFormViewModel
    {
        public int CompetitionId { get; set; }

        public ProblemFormViewModel(int competitionId)
        {
            CompetitionId = competitionId;
        }
    }
}