using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface IProblemRepository
    {
        IQueryable<Problem> Problems { get; }
        Problem Save(Problem problem);
        void Delete(Problem problem);
    }

    public class ProblemRepository : IProblemRepository
    {

        private readonly ContestEntities _contestEntities;

        public ProblemRepository(ContestEntities contestEntities)
        {
            _contestEntities = contestEntities;
        }

        public IQueryable<Problem> Problems
        {
            get { return _contestEntities.Problems; }
        }

        public Problem Save(Problem entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<Problem>().Add(entity);
            else
            {
                var problem = _contestEntities.Problems.Single(x => x.Id == entity.Id);
                _contestEntities.Entry(problem).CurrentValues.SetValues(entity);
                //_contestEntities.Set<Problem>().Attach(entity);
            }
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(Problem entity)
        {
            _contestEntities.Set<Problem>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
