using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Domain;
using CC.Domain.CodeCompilers;
using CC.Service;
using CC.Domain.Entities;
using CC.Web.Binders;
using CC.Web.Models.Problem;
using CC.Web.Models.Solution;
using File = CC.Domain.Entities.File;

namespace CC.Web.Controllers
{
    public class SolutionController : Controller
    {
        public string ServerDirectory = @"C:\fakeserver";
        public ISolutionService SolutionService { get; set; }
        public IPersonService PersonService { get; set; }
        public IProblemService ProblemService;
        public ITeamService TeamService;

        public SolutionController(ISolutionService solutionService, IPersonService personService, IProblemService problemService, ITeamService teamService)
        {
            PersonService = personService;
            SolutionService = solutionService;
            ProblemService = problemService;
            TeamService = teamService;
        }

        public ActionResult Details(int id)
        {
            var solution = SolutionService.ById(id);
            return View(new SolutionViewModel(solution));
        }

        public PartialViewResult UploadSolutionPartial(ProblemViewModel model)
        {
            var user = UserSession.LoggedInUser;
            var teams = PersonService.TeamsIn(user.Id);
            var teamsInCompetition = model.Competition.TeamInCompetitions;
            var teamid = 0;

            foreach (var t in teams)
            {
                foreach (var tic in teamsInCompetition)
                {
                    if (t.Id == tic.TeamId)
                        teamid = t.Id;
                }
            }

            var viewmodel = new UploadSolutionViewModel { ProblemId = model.Id, TeamId = teamid };

            return PartialView(viewmodel);
        }

        public ActionResult SubmitSolution(HttpPostedFileBase solution, int problemId, int teamId)
        {
            Directory.CreateDirectory(ServerDirectory + @"\fileuploads\" + problemId + "\\" + teamId);
            solution.SaveAs(ServerDirectory + @"\fileuploads\" + problemId + "\\" + teamId + "\\" + solution.FileName);
            var problem = ProblemService.ById(problemId);
            var team = TeamService.ById(teamId);

            var m = new SolutionModel
                        {
                            ProblemId = problemId,
                            TeamId = teamId,
                            SubmissionTime = DateTime.Now,
                            FileName = solution.FileName,
                            ExpectedOutput = problem.ExpectedOutput,
                            ProblemShortName = problem.ShortName,
                            SubmissionTimeout = problem.Competition.Setting.SubmissionTimeLimit,
                            ActualInput = problem.ActualInput,
                            TimesAttemped = team.Solutions.Count(x => x.ProblemId == problemId),
                            CompetitionStartTime = problem.Competition.Setting.StartTime,
                            PenaltyAmount = problem.Competition.Setting.PentaltyAmount
                        };

            CodeCompiler.CompileAndRunFile(m);

            var f = new File { Data = m.SourceCode, Name = m.FileName};
            

            var s = new Solution
                      {
                          JudgeTime = m.JudgeTime,
                          ProblemId = m.ProblemId,
                          Result = m.Result,
                          ResultDescription = m.ResultDescription,
                          Score = m.Score,
                          SourceFile = f,
                          SubmissionTime = m.SubmissionTime,
                          Team = team,
                          TeamId = teamId
                      };
            SolutionService.Save(s);
            SolutionService.SaveFile(f);
            
            return RedirectToAction("Details","Problem", new{id=problemId});
        }

        public PartialViewResult DisplayAllSolutionsPartial(ProblemViewModel model)
        {
            var team = GetTeamInCompetition(model.Competition);
            var viewmodel = new DisplaySolutionsViewModel { Problem = model, Team = team };
            viewmodel.Solutions = team.Solutions.Where(x => x.ProblemId == model.Id);
            return PartialView(viewmodel);
        }

        private Team GetTeamInCompetition(Competition c)
        {
            var teamsInCompetition = c.TeamInCompetitions;
            var loggedInUserTeams = PersonService.TeamsIn(UserSession.LoggedInUser.Id);

            return (from tic in teamsInCompetition from t in loggedInUserTeams where t.Id == tic.TeamId select t).FirstOrDefault();
        }

        [HttpPost]
        public ActionResult Create(SolutionViewModel solution)
        {
            if (ModelState.IsValid)
            {
                Save(solution);
                return RedirectToAction("Index");
            }
            return View(solution);
        }

        [HttpPost]
        public ActionResult Edit(SolutionViewModel solution)
        {
            return Create(solution);
        }

        private void Save(SolutionViewModel solution)
        {
            var solutionEntity = SolutionService.ById(solution.Id) ?? new Solution();

            solutionEntity.Result = solution.Result;
            solutionEntity.Score = solution.Score;
            solutionEntity.SourceFile = solution.Source;
            solutionEntity.Team = solution.Team;
            solutionEntity.SubmissionTime = solution.Time;

            SolutionService.Save(solutionEntity);
        }
    }
}
