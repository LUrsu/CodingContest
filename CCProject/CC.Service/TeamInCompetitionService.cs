using System.Collections.Generic;
using System.Linq;
using CC.Domain.Entities;
using CC.Domain.Repositories;

namespace CC.Service
{
    public interface ITeamInCompetitionService
    {
        TeamInCompetition ById(int id);
        TeamInCompetition ByCompetitionTeamId(int competitionId, int teamId);
        TeamInCompetition Save(TeamInCompetition teamInCompetition);
        IEnumerable<TeamInCompetition> All();
    }

    public class TeamInCompetitionService : ITeamInCompetitionService
    {
        public ITeamInCompetitionRepository TeamInCompetitionRepository { get; set; }

        public TeamInCompetitionService(ITeamInCompetitionRepository teamInCompetitionRepository)
        {
            TeamInCompetitionRepository = teamInCompetitionRepository;
        }

        public TeamInCompetition ById(int id)
        {
            return TeamInCompetitionRepository.TeamInCompetitions.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TeamInCompetition> All()
        {
            return TeamInCompetitionRepository.TeamInCompetitions;
        }

        public TeamInCompetition ByCompetitionTeamId(int competitionId, int teamId)
        {
            return TeamInCompetitionRepository.TeamInCompetitions.FirstOrDefault(t => t.CompetitionId == competitionId && t.TeamId == teamId);
        }

        public TeamInCompetition Save(TeamInCompetition teamInCompetition)
        {
            return TeamInCompetitionRepository.Save(teamInCompetition);
        }
    }
}
