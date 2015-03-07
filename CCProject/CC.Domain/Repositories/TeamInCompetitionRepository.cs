using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface ITeamInCompetitionRepository
    {
        IQueryable<TeamInCompetition> TeamInCompetitions { get; }
        TeamInCompetition Save(TeamInCompetition teamInCompetition);
        void Delete(TeamInCompetition teamInCompetition);
    }

    public class TeamInCompetitionRepository : ITeamInCompetitionRepository
    {
        private readonly ContestEntities _contestEntities;

        public TeamInCompetitionRepository(ContestEntities contestEntities)
        {
            _contestEntities = contestEntities;
        }

        public IQueryable<TeamInCompetition> TeamInCompetitions
        {
            get { return _contestEntities.TeamInCompetitions; }
        }

        public TeamInCompetition Save(TeamInCompetition entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<TeamInCompetition>().Add(entity);
            else
                _contestEntities.Set<TeamInCompetition>().Attach(entity);
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(TeamInCompetition entity)
        {
            _contestEntities.Set<TeamInCompetition>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
