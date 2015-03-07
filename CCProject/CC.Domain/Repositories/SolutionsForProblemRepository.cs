using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    /**
    public interface ISolutionsForProblemRepository
    {
        IQueryable<SolutionsForProblem> SolutionsForProblems { get; }
        SolutionsForProblem Save(SolutionsForProblem solutionsForProblem);
        void Delete(SolutionsForProblem solutionsForProblem);
    }

    public class SolutionsForProblemRepository : ISolutionsForProblemRepository
    {
        private readonly ContestEntities _contestEntities;

        public SolutionsForProblemRepository(ContestEntities contestEntities)
        {
            _contestEntities = contestEntities;
        }

        public IQueryable<SolutionsForProblem> SolutionsForProblems
        {
            get { return _contestEntities.SolutionsForProblems; }
        }

        public SolutionsForProblem Save(SolutionsForProblem entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<SolutionsForProblem>().Add(entity);
            else
                _contestEntities.Set<SolutionsForProblem>().Attach(entity);
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(SolutionsForProblem entity)
        {
            _contestEntities.Set<SolutionsForProblem>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
     */
}
