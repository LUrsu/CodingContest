using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Service;
using CC.Web.Models.Solution;
using CC.Web.Models.SolutionsForProblem;
using CC.Web.Models.TeamInCompetition;

namespace CC.Web.Controllers
{
    public class TeamInCompetitionController : Controller
    {
        
        public ITeamInCompetitionService TeamInCompetitionService { get; set; }
        public ITeamService TeamService { get; set; }
        public ICompetitionService CompetitionService { get; set; }

        public TeamInCompetitionController(ITeamInCompetitionService teamInCompetitionService, ITeamService teamService, ICompetitionService competitionService)
        {
            TeamInCompetitionService = teamInCompetitionService;
            TeamService = teamService;
            CompetitionService = competitionService;
        }

        public ActionResult Details(int id)
        {
            var teamInCompetition = TeamInCompetitionService.ById(id);
            return View(new TeamInCompetitionViewModel(teamInCompetition));
        }

        /**
        public PartialViewResult SolutionForProblemPartial(int id)
        {
            var problem = SolutionsForProblemService.GetProblem(id);
            var topSolution = SolutionsForProblemService.TopSolution(id);
            return PartialView("ListedSolutionForProblemPartial", new SolutionsForProblemViewModel(topSolution, problem));
        }

        public PartialViewResult ListedSolutionsPartial(int id)
        {
            var solutions = SolutionsForProblemService.SolutionsFromTeam(id);
            return PartialView("ListedSolutionsPartial", new SolutionsViewModel(solutions));
        }
         */

        public PartialViewResult ListedPartial(int id)
        {
            var teamInCompetition = TeamInCompetitionService.ById(id);
            var team = TeamService.ById(teamInCompetition.TeamId);
            var competition = CompetitionService.ById(teamInCompetition.CompetitionId);
            return PartialView("DisplayTemplates/ListedTeamInCompetitionViewModel",
                               new ListedTeamInCompetitionViewModel(team, competition, id));
        }
        
    }
}
