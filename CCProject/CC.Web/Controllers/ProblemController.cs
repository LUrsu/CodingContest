using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Service;
using CC.Web.Models.Problem;
using CC.Domain.Entities;
using CC.Web.Binders;

namespace CC.Web.Controllers
{
    public class ProblemController : Controller
    {
        public IProblemService ProblemService { get; set; }
        public ICompetitionService CompetitionService { get; set; }
        public IPersonService PersonService{ get; set; }

        public ProblemController(IProblemService problemService, ICompetitionService competitionService, IPersonService personService)
        {
            ProblemService = problemService;
            CompetitionService = competitionService;
            PersonService = personService;
        }

        public ActionResult Index()
        {
            var problems = ProblemService.All();
            return View(new ProblemsViewModel(problems));
        }

        public ActionResult Details(int id)
        {
            var user = UserSession.LoggedInUser;
            var teams = PersonService.TeamsIn(user.Id);
            var teamsInCompetition = ProblemService.ById(id).Competition.TeamInCompetitions;

            foreach(var t in teams)
            {
                foreach(var tic in teamsInCompetition)
                {
                    if(t.Id == tic.TeamId)
                    {
                        var problem = ProblemService.ById(id);
                        return View(new ProblemViewModel(problem));
                    }
                }
            }
            return RedirectToAction("Details", "Competition", new {id = ProblemService.ById(id).Competition.Id});

        }

        public ActionResult CreateForm(int competitionId)
        {
            return View(new ProblemViewModel(competitionId));
        }

        [HttpPost]
        public ActionResult Create(ProblemViewModel problem)
        {
            if (ModelState.IsValid)
            {
                Save(problem);
                return RedirectToAction("Index");
            }
            return View(problem);
        }

        private void Save(ProblemViewModel problem)
        {
            var problemEntity = new Problem()
            {
                CompetitionId = problem.CompetitionId,
                Description = problem.Description,
                ExampleInput = problem.ExampleInput,
                ExampleOutput = problem.ExampleOutput,
                ExpectedOutput = problem.ExpectedOutput,
                Name = problem.Name,
                ShortName = problem.ShortName
            };

            ProblemService.Save(problemEntity);
        }

        public ActionResult Edit(int id)
        {
            var model = new ProblemViewModel(ProblemService.ById(id));
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ProblemViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var competition = CompetitionService.ById(model.CompetitionId);
            var problem = ProblemService.ById(model.Id);

            problem.ActualInput = model.ActualInput;
            problem.ExampleInput = model.ExampleInput;
            problem.Description = model.Description;
            problem.ExampleOutput = model.ExampleOutput;
            problem.ExpectedOutput = model.ExpectedOutput;
            problem.Name = model.Name;
            problem.ShortName = model.ShortName;


            CompetitionService.SaveProblem(problem);
            return RedirectToAction("Details","Competition",new { id = competition.Id });
        }
    }
}
