using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CC.Domain.Entities;
using CC.Domain.Repositories;
using CC.Service.DataHolders;

namespace CC.Service
{
    public interface ICompetitionService
    {
        Competition ById(int id);
        IEnumerable<Team> AllTeamsInCompetition(int contestId);
        Competition Save(Competition competition);
        Problem SaveProblem(Problem problem);
        void AttachProblem(int contestId, Problem problem);
        void AttachSettings(int contestId, Setting setting);
        bool IsPersonRegisterForCompetition(int competitionId, int personId);
        IEnumerable<Competition> OnGoingCompetitions();
        IEnumerable<Competition> Previous(int number);
        IEnumerable<Competition> UpComing(int number);
        IEnumerable<Competition> PersonsCompetitions(Person person);
        IEnumerable<Competition> PersonsOnGoingCompetitions(int personId);
        Competition RegisterTeam(int id, int teamId);
        Competition LogInTeam(string competitionName, string password);

        void DeleteProblem(Problem problem);

        void DeleteCompetition(int id);

        IEnumerable<Domain.Entities.Competition> All();
    }

    public class CompetitionService : ICompetitionService
    {
        private ICompetitionRepository CompetitionRepository { get; set; }
        private IProblemRepository ProblemRepository { get; set; }
        public ISettingRepository SettingRepository { get; set; }
        public IPersonService PersonService { get; set; }
        public ITeamInCompetitionService TeamInCompetitionService { get; set; }
        private ITeamRepository TeamRepository { get; set; }

        public CompetitionService(ITeamRepository teamRepository, ICompetitionRepository competitionRepository, IProblemRepository problemRepository, ISettingRepository settingRepository, IPersonService personService, ITeamInCompetitionService teamInCompetitionService)
        {
            TeamRepository = teamRepository;
            CompetitionRepository = competitionRepository;
            ProblemRepository = problemRepository;
            SettingRepository = settingRepository;
            PersonService = personService;
            TeamInCompetitionService = teamInCompetitionService;
        }

        public IEnumerable<Domain.Entities.Competition> All()
        {
            return CompetitionRepository.Competitions;
        }

        public Competition ById(int id)
        {
            return CompetitionRepository.Competitions.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Team> AllTeamsInCompetition(int contestId)
        {
            var allTeams = TeamRepository.Teams.Select(x => x);
            var competition = ById(contestId);
            var allTeamsInCompetition = competition.TeamInCompetitions;
            return from t in allTeams from tc in allTeamsInCompetition where t.Id == tc.Id select t;
        }

        public Competition Save(Competition competition)
        {
            return CompetitionRepository.Save(competition);
        }

        public void DeleteCompetition(int id)
        {
            var competition = ById(id);
            
            CompetitionRepository.Delete(competition);
        }

        public Problem SaveProblem(Problem problem)
        {
            return ProblemRepository.Save(problem);
        }

        public void AttachProblem(int contestId, Problem problem)
        {
            var contest = ById(contestId);
            contest.Problems.Add(problem);
            ProblemRepository.Save(problem);
            Save(contest);
        }

        public void DeleteProblem(Problem problem)
        {
            var competition = ById(problem.CompetitionId);
            problem = competition.Problems.First(p => p.Id == problem.Id);
            competition.Problems.Remove(problem);
            ProblemRepository.Delete(problem);
            Save(competition);
        }

        public void AttachSettings(int contestId, Setting setting)
        {
            var contest = ById(contestId);
            contest.Setting = setting;
            Save(contest);
            SettingRepository.Save(setting);
        }

        public bool IsPersonRegisterForCompetition(int competitionId, int personId)
        {
            var competition = ById(competitionId);
            var person = PersonService.ById(personId);
            return competition.TeamInCompetitions.Any(t =>
                                                          {
                                                              var firstOrDefault = TeamRepository.Teams.FirstOrDefault(te => te.Id == t.TeamId);
                                                              return firstOrDefault != null && firstOrDefault.People.Contains(person);
                                                          });
        }

        public IEnumerable<Competition> OnGoingCompetitions()
        {
            var comps =
                CompetitionRepository.Competitions.Where(
                    c => c.Setting.StartTime < DateTime.Now && c.Setting.EndTime > DateTime.Now);
            return comps.AsEnumerable();
        }

        public IEnumerable<Competition> Previous(int number)
        {
            var finishedComp = CompetitionRepository.Competitions.Where(c => c.Setting.EndTime < DateTime.Now);
            if (number == 0)
                return finishedComp.OrderByDescending(c => c.Setting.EndTime).Take(number);
            else
                return finishedComp.OrderByDescending(c => c.Setting.EndTime);
        }

        public IEnumerable<Competition> UpComing(int number)
        {
            var upcoming = CompetitionRepository.Competitions.Where(c => c.Setting.StartTime > DateTime.Now);
            if (number == 0)
                return upcoming.OrderByDescending(c => c.Setting.StartTime);
            else
                return upcoming.OrderByDescending(c => c.Setting.StartTime).Take(number);
        }

        public IEnumerable<Competition> PersonsCompetitions(Person person)
        {
            var teams = person.Teams;
            var allCompetitions = CompetitionRepository.Competitions;
            var allTeamsInCompetitions = TeamInCompetitionService.All();

            return from teamCompetition in allTeamsInCompetitions from team in teams from competition in allCompetitions where teamCompetition.TeamId == team.Id && teamCompetition.CompetitionId == competition.Id select competition;

        }

        public IEnumerable<Competition> PersonsOnGoingCompetitions(int personId)
        {
            var personsCompetitions = this.PersonsCompetitions(PersonService.ById(personId));
            return personsCompetitions.Where(c => c.Setting.EndTime > DateTime.Now && c.Setting.StartTime < DateTime.Now).Take(3);
        }

        public Competition RegisterTeam(int id, int teamId)
        {
            var team = PersonService.ByTeamId(teamId);
            var competition = ById(id);
            if (team != null)
            {
                var teamInCompetition = new TeamInCompetition
                                            {
                                                CompetitionId = competition.Id,
                                                TeamId = team.Id
                                            };
                team.TeamInCompetitions.Add(teamInCompetition);
                competition.TeamInCompetitions.Add(teamInCompetition);
                TeamInCompetitionService.Save(teamInCompetition);
                PersonService.SaveTeam(team);
                competition = CompetitionRepository.Save(competition);
            }
            return competition;
        }

        public Competition LogInTeam(string competitionName, string password)
        {
            return CompetitionRepository.Competitions.FirstOrDefault(c => c.Name == competitionName && c.EntryPassword == password);
        }
    }
}
