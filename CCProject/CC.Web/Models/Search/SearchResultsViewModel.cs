using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Web.Models.Competition;
using CC.Web.Models.Person;
using CC.Web.Models.Team;

namespace CC.Web.Models.Search
{
    public class SearchResultsViewModel
    {
        public ListedTeamsViewModel ListedTeamsViewModel { get; set; }

        public PeopleViewModel PeopleViewModel { get; set; }

        public CompetitionsViewModel CompetitionsViewModel { get; set; }

        public SearchResultsViewModel(ListedTeamsViewModel listedTeamViewModle, PeopleViewModel peopleViewModel, CompetitionsViewModel competitionsViewModel)
        {
            ListedTeamsViewModel = listedTeamViewModle;
            PeopleViewModel = peopleViewModel;
            CompetitionsViewModel = competitionsViewModel;
        }
    }
}