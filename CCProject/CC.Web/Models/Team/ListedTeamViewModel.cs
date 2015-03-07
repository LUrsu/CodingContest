using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Team
{
    public class ListedTeamViewModel
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public ListedTeamViewModel(Domain.Entities.Team team)
        {
            Name = team.Name;
            Id = team.Id;
        }

        public ListedTeamViewModel()
        {
            // TODO: Complete member initialization
        }
    }
}