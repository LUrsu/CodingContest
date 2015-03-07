using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface ISolutionRepository
    {
        IQueryable<Solution> Solutions { get; }
        Solution Save(Solution solution);
        File SaveFile(File entity);
        void Delete(Solution solution);
    }

    public class SolutionRepository : ISolutionRepository
    {

        private readonly ContestEntities _contestEntities;

        public SolutionRepository(ContestEntities contestEntities)
        {
            _contestEntities = contestEntities;
        }

        public IQueryable<Solution> Solutions
        {
            get { return _contestEntities.Solutions; }
        }

        public Solution Save(Solution entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<Solution>().Add(entity);
            else
                _contestEntities.Set<Solution>().Attach(entity);
            _contestEntities.SaveChanges();
            return entity;
        }

        public File SaveFile(File entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<File>().Add(entity);
            
            else
                _contestEntities.Set<File>().Attach(entity);
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(Solution entity)
        {
            _contestEntities.Set<Solution>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
