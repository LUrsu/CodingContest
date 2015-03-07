using System.Web.Mvc;
using System.Web.Security;
using CC.Domain.Entities;
using System.Linq;
using CC.Service;
using CC.Web.Binders;
using CC.Web.Models.Competition;
using CC.Web.Models.Person;
using CC.Web.Models.Team;

namespace CC.Web.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        public ITeamService TeamService { get; set; }
        public ICompetitionService CompetitionService { get; set; }

        public TeamController(ITeamService teamService, ICompetitionService competitionService)
        {
            TeamService = teamService;
            CompetitionService = competitionService;
        }

        public ActionResult Details(int id)
        {
            var team = TeamService.ById(id);
            Competition competition = null;
            var onTeam = false;
            if(team != null)
            {
                var teamInCompetition = team.TeamInCompetitions;
                if (teamInCompetition.Any())
                {
                    onTeam = TeamService.IsPersonOnTeam(team, UserSession.LoggedInUser.Id);
                    competition = CompetitionService.ById(teamInCompetition.FirstOrDefault().CompetitionId);
                }
            }
            
            var teamView = new TeamViewModel(team, competition, onTeam);
            return View(teamView);
        }

        public PartialViewResult TeamsPeople(int id)
        {
            var people = TeamService.ById(id).People;
            return PartialView(new PeopleViewModel(people));
        }

        public PartialViewResult TeamCompetitionsPartial(int id)
        {
            var upcoming = TeamService.TeamsUpComingCompetitions(id, 20);
            var ongoing = TeamService.TeamsOnGoingCompetitions(id);
            var completed = TeamService.TeamsCompleatedCompetions(id, 20);
            var teamCompetitionView = new TeamCompetitionViewModel(upcoming, ongoing, completed);
            return PartialView(teamCompetitionView);
        }

        [Authorize]
        public ActionResult EditorFor(int teamId)
        {
            var team = TeamService.ById(teamId);
            var teamModel = new TeamViewModel(team, null, false);
            return View(teamModel);
        }

        [Authorize]
        public ActionResult CreateForm(int competitionId)
        {
            var allTeamsInCompetition = CompetitionService.ById(competitionId).TeamInCompetitions;

            if (allTeamsInCompetition.Select(t => TeamService.ById(t.TeamId)).SelectMany(lt => lt.People).Any(m => m.Id == UserSession.LoggedInUser.Id))
                return RedirectToAction("Details","Competition",new {id = competitionId});

            return View(new TeamViewModel{CompetitionId = competitionId});
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(TeamViewModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateForm", model);
            var team = TeamService.CreateTeam(model.Name, UserSession.LoggedInUser.Id, model.Password);
            var competition = CompetitionService.ById(model.CompetitionId);
            var teamInCompetition = TeamService.RegisterTeamForCompetition(competition.Id, team.Id);
            return RedirectToAction("Details", new {team.Id});
        }

        [HttpPost, Authorize]
        public ActionResult Edit(TeamViewModel model)
        {
            var team = TeamService.ById(model.Id);
            team.Name = model.Name;
            TeamService.Save(team);
            return RedirectToAction("Details", new {model.Id});
        }

        [Authorize]
        public PartialViewResult JoinButton(int id)
        {
            var team = TeamService.ById(id);
            var allTeamsInCompetition = CompetitionService.ById(team.TeamInCompetitions.Single(x => x.TeamId == team.Id).CompetitionId).TeamInCompetitions;

            if (allTeamsInCompetition.Select(t => TeamService.ById(t.TeamId)).SelectMany(lt => lt.People).Any(m => m.Id == UserSession.LoggedInUser.Id))
                return null;

            return PartialView(new AddPersonViewModel(id, UserSession.LoggedInUser.UserName));
        }

        [Authorize]
        public PartialViewResult LeaveButton(int id)
        {
            return PartialView(new AddPersonViewModel(id, UserSession.LoggedInUser.UserName));
        }

        [HttpPost, Authorize]
        public ActionResult AddPerson(AddPersonViewModel model)
        {
            TeamService.AddPersonToTeam(model.TeamId, model.UserName, model.Password);
            return RedirectToAction("Details", new {id = model.TeamId});
        }

        [HttpPost, Authorize]
        public ActionResult RemovePerson(AddPersonViewModel model)
        {
            TeamService.RemovePersonToTeam(model.TeamId, model.UserName);
            return RedirectToAction("Details", new { id = model.TeamId });
        }

        [HttpPost, Authorize]
        public ActionResult RegisterCompetition(CompetitionRegistrationViewModel competitionRegistration)
        {
            var competionId = TeamService.LogAndRegisterForCompetition(competitionRegistration.CompetitionName,
                                                                       competitionRegistration.PassWord,
                                                                       competitionRegistration.TeamId);
            if (competionId >= 0)
            {
                return RedirectToAction("Details", new { controller = "Competition", id = competionId });
            }
            else
            {
                ModelState.AddModelError("", "could not log in to competition");
                //TODO fix this error redirect to were we actually want it to go
                return View(competitionRegistration);
            }
        }
    }
}
