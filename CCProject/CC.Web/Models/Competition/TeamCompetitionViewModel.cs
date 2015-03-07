using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Competition
{
    public class TeamCompetitionViewModel
    {
        public CompetitionsViewModel UpComing { get; set; }

        public CompetitionsViewModel OnGoing { get; set; }

        public CompetitionsViewModel Compleated { get; set; }

        public TeamCompetitionViewModel(IEnumerable<Domain.Entities.Competition> upComing, IEnumerable<Domain.Entities.Competition> onGoing, IEnumerable<Domain.Entities.Competition> compleated)
        {
            UpComing = new CompetitionsViewModel(upComing);
            OnGoing = new CompetitionsViewModel(onGoing);
            Compleated = new CompetitionsViewModel(compleated);
        }

    }
}