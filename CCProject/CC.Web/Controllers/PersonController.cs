using System;
using System.Web.Mvc;
using System.Linq;
using System.Web.Security;
using CC.Domain.Entities;
using CC.Service;
using CC.Web.Binders;
using CC.Web.Models;
using CC.Web.Models.Competition;
using CC.Web.Models.Person;
using CC.Web.Models.Team;

namespace CC.Web.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {

        public IPersonService PersonService { get; set; }
        public ITeamService TeamService { get; set; }

        public PersonController(IPersonService personService, ITeamService teamService)
        {
            PersonService = personService;
            TeamService = teamService;
        }

        public ActionResult Index(string username)
        {
            var person = PersonService.ByUserName(username);
            return person != null ? RedirectToAction("Details", new { id = person.Id }) : RedirectToAction("CreateForm", new {userName = username});
        }

        public ActionResult Detail(string userName)
        {
            var person = PersonService.ByUserName(userName);
            return RedirectToAction("Details", new {id = person.Id});
        }

        public ActionResult Details(int id)
        {
            var person = PersonService.ById(id);
            var isLoggedInUser = (person != null && person.UserName == Membership.GetUser().UserName);
            return View(new PersonViewModel(person, isLoggedInUser));
        }

        public PartialViewResult PersonsTeams(int id)
        {
            var teams = PersonService.ById(id).Teams;
            return PartialView(new ListedTeamsViewModel(teams));
        }

        public PartialViewResult PersonsTeamsInCompetitions(int id)
        {
            var teamIds = PersonService.TeamsIn(id).Select(t => t.Id).ToList();
            return PartialView(new ListedIdsViewModel(teamIds));
        }

        public PartialViewResult PersonsCompetitions(int id)
        {
            var personsCompetitions = TeamService.TeamsCompetitions(PersonService.ById(id));
            return PartialView(new CompetitionsViewModel(personsCompetitions));
        }

        public ActionResult CreateForm(string userName)
        {
            var model = new PersonFormViewModel(userName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PersonFormViewModel model)
        {
            if(!ModelState.IsValid)
                return View("CreateForm",model);
            var person = new Person
                             {
                                 UserName = model.UserName,
                                 FirstName = model.FirstName,
                                 LastName = model.LastName,
                                 Email = model.Email,
                                 Role = "Student"
                             };
            var newPerson = PersonService.Save(person);
            UserSession.LoggedInUser = person;
            return RedirectToAction("Detail", new { userName = newPerson.UserName });
        }
    }
}
