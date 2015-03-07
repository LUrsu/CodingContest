using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.TeamInCompetition
{
    public class ListedTeamInCompetitionViewModel
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public ListedTeamInCompetitionViewModel(Domain.Entities.Team team, Domain.Entities.Competition competition, int id)
        {
            Id = id;
            DisplayName = team.Name + " in " + competition.Name;
        }
    }
}