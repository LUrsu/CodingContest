using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Team
{
    public class ListedTeamsViewModel
    {
        public IEnumerable<ListedTeamViewModel> ListedTeamViews { get; set; }

        public ListedTeamsViewModel(IEnumerable<Domain.Entities.Team> teams)
        {
            ListedTeamViews = teams.ToList().ConvertAll(t => new ListedTeamViewModel(t));
        }
    }
}