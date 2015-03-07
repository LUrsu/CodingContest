using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> All();
        TEntity Save(TEntity entity);
        void Delete(TEntity entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ContestEntities _contestEntities;

        protected Repository()
        {
            _contestEntities = new ContestEntities();
        }

        public IQueryable<TEntity> All()
        {
            return _contestEntities.Set<TEntity>();
        }

        public TEntity Save(TEntity entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<TEntity>().Add(entity);
            else
                _contestEntities.Set<TEntity>().Attach(entity);
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _contestEntities.Set<TEntity>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
