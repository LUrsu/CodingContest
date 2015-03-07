using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface ICompetitionRepository
    {
        IQueryable<Competition> Competitions { get; }
        Competition Save(Competition competition);
        void Delete(Competition competition);
    }

    public class CompetitionRepository : ICompetitionRepository
    {

        private readonly ContestEntities _contestEntities;

        public CompetitionRepository(ContestEntities context)
        {
            _contestEntities = context;
        }

        public IQueryable<Competition> Competitions
        {
            get { return _contestEntities.Competitions; }
        }

        public Competition Save(Competition entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<Competition>().Add(entity);
            else
            {
                var competition = _contestEntities.Competitions.Single(x => x.Id == entity.Id);
                _contestEntities.Entry(competition).CurrentValues.SetValues(entity);
                //_contestEntities.Set<Competition>().Attach(entity);
            }
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(Competition entity)
        {
            _contestEntities.Set<Competition>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
