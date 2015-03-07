using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Domain.Repositories;
using CC.Service;
using CC.Web.Binders;
using CC.Web.Models.Competition;
using CC.Web.Models.Problem;
using CC.Web.Models.Team;
using System.Web.Security;
using CC.Domain.Entities;
using CC.Web.Models.Setting;

namespace CC.Web.Controllers
{

    public class CompetitionController : Controller
    {

        public ITeamService TeamService { get; set; }
        public IPersonService PersonService { get; set; }
        public ICompetitionService CompetitionService { get; set; }
        public ISettingRepository SettingRepository { get; set; }

        public CompetitionController(ITeamService teamService, IPersonService personService, ICompetitionService competitionService, ISettingRepository settingRepository)
        {
            TeamService = teamService;
            PersonService = personService;
            CompetitionService = competitionService;
            SettingRepository = settingRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            var competitions = new CompetitionsViewModel(CompetitionService.All());
            return View(competitions);
        }

        /**
         * [Authorize]
        public ActionResult Details(int id)
        {
            var competition = CompetitionService.ById(id);
            var isOnGoing = CompetitionService.OnGoingCompetitions().Contains(competition);
            var personsTeams = PersonService.ByUserName(Membership.GetUser().UserName).Teams;
            var isRegistered = personsTeams.Any(t =>{
                            var teamInCompetition = t.TeamInCompetitions.FirstOrDefault();
                            return teamInCompetition != null && teamInCompetition.CompetitionId == competition.Id;
                        });
            var competitionView = new CompetitionViewModel(competition, isOnGoing, isRegistered);
            return View(competitionView);
        }
         */

        [Authorize]
        public ActionResult Details(int id)
        {
            var competition = CompetitionService.ById(id);
            var admin = PersonService.ById(competition.AdminId);
            var teams = CompetitionService.AllTeamsInCompetition(id);
            var personIsRegistered = CompetitionService.IsPersonRegisterForCompetition(id, UserSession.LoggedInUser.Id);
            var isOnGoing = CompetitionService.OnGoingCompetitions().Contains(competition);
            var model = new CompetitionViewModel(competition) { Admin = admin, RegisteredTeams = teams, IsOngoing = isOnGoing, IsPersonRegistered = personIsRegistered};
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var competition = CompetitionService.ById(id);
            var isOnGoing = CompetitionService.OnGoingCompetitions().Contains(competition);
            var personsTeams = PersonService.ByUserName(Membership.GetUser().UserName).Teams;
            var isRegistered = personsTeams.Any(t =>
            {
                var teamInCompetition = t.TeamInCompetitions.FirstOrDefault();
                return teamInCompetition != null && teamInCompetition.CompetitionId == competition.Id;
            });
            var competitionView = new CompetitionEditViewModel(competition);
            competitionView.PenaltyAmount = competition.Setting.PentaltyAmount;
            competitionView.SubmissionTimeout = competition.Setting.SubmissionTimeLimit;
            return View(competitionView);
        }


        [Authorize]
        public ActionResult Delete(int id)
        {
            CompetitionService.DeleteCompetition(id);
            return RedirectToAction("Index");
        }
        public PartialViewResult OngoingCompetitions()
        {
            var ongoingCompetitions = new CompetitionsViewModel(CompetitionService.OnGoingCompetitions());
            return PartialView("CompetitionsPartial", ongoingCompetitions);
        }

        public PartialViewResult UpcomingCompetitions()
        {
            var upcomingCompetitions = new CompetitionsViewModel(CompetitionService.UpComing(3));
            return PartialView("CompetitionsPartial", upcomingCompetitions);
        }

        public PartialViewResult PreviousCompetitions()
        {
            var previousCompetitions = new CompetitionsViewModel(CompetitionService.Previous(3));
            return PartialView("CompetitionsPartial", previousCompetitions);
        }

        [Authorize]
        public PartialViewResult RegisteredTeams(int id)
        {
            var competitionTeams = TeamService.RegisteredTeams(id);
            return PartialView("ListedTeamsPartial", new ListedTeamsViewModel(competitionTeams));
        }

        [HttpPost]
        [Authorize]
        public ActionResult AttachProblem(ProblemViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Details", new { id = model.CompetitionId });
            var competition = CompetitionService.ById(model.CompetitionId);
            var problem = new Problem
                              {
                                  Competition = competition,
                                  CompetitionId = model.CompetitionId,
                                  Name = model.Name,
                                  ShortName = model.ShortName,
                                  Description = model.Description,
                                  ExampleInput = model.ExampleInput,
                                  ExampleOutput = model.ExampleOutput,
                                  ExpectedOutput = model.ExpectedOutput,
                                  ActualInput = model.ActualInput
                              };
            CompetitionService.SaveProblem(problem);
            competition.Problems.Add(problem);
            CompetitionService.Save(competition);
            return RedirectToAction("Edit", new { id = competition.Id });
        }

        [Authorize]
        public ActionResult DeleteProblem(int problemID, int competitionID)
        {
            var competition = CompetitionService.ById(competitionID);
            var problem = competition.Problems.First(p => p.Id == problemID);
            CompetitionService.DeleteProblem(problem);

            return RedirectToAction("Edit", new { id = competition.Id });
        }

        [Authorize]
        public ActionResult ScoreBoard(int id)
        {
            var competiton = CompetitionService.ById(id);
            return View(new CompetitionViewModel(competiton));
        }

        public PartialViewResult ScoreBoardPartial(int id)
        {
            var scoreBoardData = TeamService.ScoreBoardsData(id);
            return PartialView(scoreBoardData);
        }

        [Authorize]
        public PartialViewResult LogInPartial(int id)
        {
            var competition = TeamService.ByCompetitionId(id);
            var person = PersonService.ById(UserSession.LoggedInUser.Id);
            var logModel = new CompetitionRegistrationViewModel(competition, person.Teams.ToList());
            return PartialView("CompetitionLogInPartial", logModel);
        }

        [HttpPost]
        [Authorize]
        public ActionResult RegisterTeam(CompetitionRegistrationViewModel model)
        {
            var competition = CompetitionService.RegisterTeam(model.Id, model.TeamId);
            return RedirectToAction("Details", new { id = competition.Id });
        }

        [Authorize]
        public ActionResult CreateForm(int settingId)
        {
            var compettion = new CompetitionFormViewModel(UserSession.LoggedInUser.Id, settingId);
            return View(compettion);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CompetitionFormViewModel model)
        {
            var setting = SettingRepository.Settings.FirstOrDefault(s => s.Id == model.SettingId);
            var competition = new Competition
                                  {
                                      AdminId = model.AdminId,
                                      EntryPassword = model.Password,
                                      Name = model.Name,
                                      Setting = setting
                                  };
            competition = CompetitionService.Save(competition);
            return RedirectToAction("Details", new { id = competition.Id });
        }

        [Authorize]
        public ActionResult SettingForm()
        {
            return View(new SettingViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateSetting(SettingViewModel model, DateTime endTime, DateTime startTime)
        {
            var setting = new Setting
            {
                StartTime = model.StartTime,
                EndTime = endTime,
                PentaltyAmount = model.PentaltyAmount,
                SubmissionTimeLimit = model.SubmissionTimeLimit
            };
            var newSetting = SettingRepository.Save(setting);
            return RedirectToAction("CreateForm", new { settingId = newSetting.Id });
        }

        [Authorize]
        public ActionResult CreateCompetition()
        {
            return View(new CompetitionAndSettingsFormViewModel());
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateCompetition(CompetitionAndSettingsFormViewModel model)
        {
            //validation
            if (model.EndTime <= model.StartTime)
            {
                ModelState.AddModelError("EndBeforeStart", "The competition end time cannot be before the start time.");
                return View("CreateCompetition");
            }
            if (model.StartTime <= DateTime.Now)
            {
                ModelState.AddModelError("StartBeforeNow", "The competition start time needs to be in the future.");
                return View("CreateCompetition");
            }

            var setting = new Setting
            {
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                PentaltyAmount = model.PenaltyAmount,
                SubmissionTimeLimit = model.SubmissionTimeout
            };
            var newSetting = SettingRepository.Save(setting);

            var competition = new Competition
            {
                AdminId = UserSession.LoggedInUser.Id,
                EntryPassword = model.Password,
                Name = model.Name,
                Setting = setting
            };
            competition = CompetitionService.Save(competition);

            return RedirectToAction("Details", new { id = competition.Id });
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditCompetition(CompetitionEditViewModel model)
        {
            //validation
            if (model.EndTime <= model.StartTime)
            {
                ModelState.AddModelError("EndBeforeStart", "The competition end time cannot be before the start time.");
                return RedirectToAction("Edit", new { id = model.Id });
            }
            if (model.Competition.Name == null)
            {
                ModelState.AddModelError("NeedName", "You need to have a name.");
                return RedirectToAction("Edit", new { id = model.Id });
            }

            if(!ModelState.IsValid)
                return RedirectToAction("Edit", new { id = model.Id });

            var competition = CompetitionService.ById(model.Id);

            competition.Setting.StartTime = model.StartTime;
            competition.Setting.EndTime = model.EndTime;
            competition.Setting.PentaltyAmount = model.PenaltyAmount;
            competition.Setting.SubmissionTimeLimit = model.SubmissionTimeout;
            var newSetting = SettingRepository.Save(competition.Setting);

            competition.Name = model.Competition.Name;

            var newCompetition = CompetitionService.Save(competition);

            return RedirectToAction("Details", new { id = model.Id });
        }
    }
}
