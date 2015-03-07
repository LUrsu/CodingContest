using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.TeamInCompetition
{
    public class TeamInCompetitionViewModel
    {
        public List<int> SolutionForProblemIds { get; set; }

        public TeamInCompetitionViewModel(Domain.Entities.TeamInCompetition teamInCompetition)
        {
            /**
            SolutionForProblemIds = teamInCompetition.SolutionsForProblems.Select(s => s.Id).ToList();
             */
        }
    }
}