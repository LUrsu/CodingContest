using System.Collections.Generic;
using System.Linq;
using CC.Domain.Entities;
using CC.Domain.Repositories;
using CC.Service.DataHolders;

namespace CC.Service
{
    public interface ITeamService
    {
        IEnumerable<Team> All();
        Team ById(int id);
        Competition ByCompetitionId(int id);
        Team ByName(string name);
        TeamInCompetition RegisterTeamForCompetition(int teamId, int competitionId);
        int LogAndRegisterForCompetition(string competitionName, string password, int teamId);
        void Save(Team team);
        bool AddPersonToTeam(int teamId, string personName, string password);
        bool IsPersonOnTeam(Team team, int personId);
        Team CreateTeam(string teamName, int posterId, string password);
        bool RemovePersonToTeam(int teamId, string personName);
        IEnumerable<Team> Search(string teamName);
        IEnumerable<Competition> TeamsOnGoingCompetitions(int teamId);
        IEnumerable<Competition> TeamsUpComingCompetitions(int teamId, int numberResults);
        IEnumerable<Competition> TeamsCompleatedCompetions(int teamId, int numberResults);
        IEnumerable<Team> TeamsByIds(IEnumerable<int> ids);
        IEnumerable<Competition> TeamsCompetitions(Person person);
        IEnumerable<Team> RegisteredTeams(int id);
        ScoreBoardData ScoreBoardsData(int id);
    }

    public class TeamService : ITeamService
    {
        private ITeamRepository TeamRepository { get; set; }
        private IPersonService PersonService { get; set; }
        public ICompetitionService CompetitionService { get; set; }
        public ITeamInCompetitionService TeamInCompetitionService { get; set; }

        public TeamService(ITeamRepository teamRepository, IPersonService personService, ICompetitionService competitionService, ITeamInCompetitionService teamInCompetitionService)
        {
            TeamRepository = teamRepository;
            PersonService = personService;
            CompetitionService = competitionService;
            TeamInCompetitionService = teamInCompetitionService;
        }

        public IEnumerable<Team> All()
        {
            return TeamRepository.Teams.Select(x => x); 
        }

        public Team ById(int id)
        {
            return TeamRepository.Teams.FirstOrDefault(t => t.Id == id);
        }

        public Competition ByCompetitionId(int id)
        {
            return CompetitionService.ById(id);
        }

        public Team ByName(string name)
        {
            return TeamRepository.Teams.FirstOrDefault(t => t.Name == name);
        }

        public TeamInCompetition RegisterTeamForCompetition(int competitionId, int teamId)
        {
            var teamInCompetition = new TeamInCompetition
                                        {
                                            TeamId = teamId,
                                            CompetitionId = competitionId
                                        };
            return TeamInCompetitionService.Save(teamInCompetition);
        }

        public int LogAndRegisterForCompetition(string competitionName, string password, int teamId)
        {
            var loggedCompetition = CompetitionService.LogInTeam(competitionName, password);
            var team = ById(teamId);
            var competitionId = 0;
            if (loggedCompetition != null && team != null)
            {
                RegisterTeamForCompetition(team.Id, loggedCompetition.Id);
                competitionId = loggedCompetition.Id;
            }
            return competitionId;
        }

        public void Save(Team team)
        {
            TeamRepository.Save(team);
        }

        public bool AddPersonToTeam(int teamId, string personName, string password)
        {
            var team = ById(teamId);
            var person = PersonService.ByUserName(personName);
            var isValid = team != null && person != null && team.Password == password;
            if(isValid)
            {
                team.People.Add(person);
                person.Teams.Add(team);
                TeamRepository.Save(team);
                PersonService.Save(person);
            }
            return isValid;
        }

        public bool RemovePersonToTeam(int teamId, string personName)
        {
            var team = ById(teamId);
            var person = PersonService.ByUserName(personName);
            var isValid = team != null && person != null;
            if (isValid)
            {
                team.People.Remove(person);
                person.Teams.Remove(team);
                TeamRepository.Save(team);
                PersonService.Save(person);
            }
            return isValid;
        }

        public bool IsPersonOnTeam(Team team, int personId)
        {
            var person = PersonService.ById(personId);
            return team.People.Contains(person);
        }

        public Team CreateTeam(string teamName, int posterId, string password)
        {
            var team = new Team
                           {
                               Name = teamName,
                               Password = password
                           };
            var person = PersonService.ById(posterId);
            team.People.Add(person);
            var newTeam = TeamRepository.Save(team);
            person.Teams.Add(newTeam);
            PersonService.Save(person);
            return newTeam;
        }

        public IEnumerable<Team> Search(string teamName)
        {
            return
                TeamRepository.Teams.Where(
                    t => t.Name.Contains(teamName) || t.Name.StartsWith(teamName) || t.Name.Equals(teamName));
        }

        public IEnumerable<Competition> TeamsOnGoingCompetitions(int teamId)
        {
            var onGoing = CompetitionService.OnGoingCompetitions();
            return onGoing.Where(c => c.TeamInCompetitions.Select(t => t.TeamId).Contains(teamId));
        }

        public IEnumerable<Competition> TeamsUpComingCompetitions(int teamId, int numberResults)
        {
            var upComing = CompetitionService.UpComing(0);
            return upComing.Where(c => c.TeamInCompetitions.Select(t => t.TeamId).Contains(teamId)).Take(numberResults);
        }

        public IEnumerable<Competition> TeamsCompleatedCompetions(int teamId, int numberResults)
        {
            var compleated = CompetitionService.Previous(0);
            return compleated.Where(c => c.TeamInCompetitions.Select(t => t.TeamId).Contains(teamId)).Take(numberResults);
        }

        public IEnumerable<Team> TeamsByIds(IEnumerable<int> ids)
        {
            return ids.Select(ById).Where(team => team != null).ToList();
        }

        public IEnumerable<Competition> TeamsCompetitions(Person person)
        {
            return CompetitionService.PersonsCompetitions(person);
        }

        public IEnumerable<Team> RegisteredTeams(int id)
        {
            var competition = CompetitionService.ById(id);
            var teamsIds = competition.TeamInCompetitions.Select(t => t.TeamId);
            return TeamsByIds(teamsIds);
        }

        public ScoreBoardData ScoreBoardsData(int id)
        {
            var competition = CompetitionService.ById(id);
            var teamsInCompetition = TeamsByIds(competition.TeamInCompetitions.Select(t => t.TeamId));
            var resultCollection = teamsInCompetition.Select(team => TeamsScore(team, competition)).ToList();
            return new ScoreBoardData(competition.Problems.ToList(), resultCollection);
        }

        public TeamsScores TeamsScore(Team team, Competition competition)
        {
            var scores = new List<ScoreBoardCell>();
            foreach (var problem in competition.Problems)
            {
                var solutionsForProblem = team.Solutions;
                var solutionForProblem = solutionsForProblem.FirstOrDefault(s => s.ProblemId == problem.Id && s.Score > 0);
                var cellData = new ScoreBoardCell(0, 0);
                if (solutionForProblem != null)
                {
                    cellData.SolutionId = solutionForProblem.Id;
                    cellData.Score = solutionForProblem.Score.Value;
                }
                scores.Add(cellData);
            }
            return new TeamsScores(scores, team);
        }
    }
}
