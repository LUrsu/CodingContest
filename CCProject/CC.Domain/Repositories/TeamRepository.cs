using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface ITeamRepository
    {
        IQueryable<Team> Teams { get; }
        Team Save(Team team);
        void Delete(Team team);
    }

    public class TeamRepository : ITeamRepository
    {

        private readonly ContestEntities _contestEntities;

        public TeamRepository(ContestEntities contestEntities)
        {
            _contestEntities = contestEntities;
        }

        public IQueryable<Team> Teams
        {
            get { return _contestEntities.Teams; }
        }

        public Team Save(Team entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Teams.Add(entity);
            else
                _contestEntities.Teams.Attach(entity);
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(Team entity)
        {
            _contestEntities.Set<Team>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
