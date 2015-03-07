using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Service;
using CC.Web.Models.Team;

namespace CC.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        public ITeamService TeamService { get; set; }

        public SearchController(ITeamService teamService)
        {
            TeamService = teamService;
        }

        public PartialViewResult Search(string query)
        {
            var results = TeamService.Search(query);
            var listedTeamsView = new ListedTeamsViewModel(results);
            return PartialView("SearchResultsPartial", listedTeamsView);
        }

    }
}
