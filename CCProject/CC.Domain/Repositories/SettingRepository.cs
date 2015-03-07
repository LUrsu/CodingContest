using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface ISettingRepository
    {
        IQueryable<Setting> Settings { get; }
        Setting Save(Setting setting);
        void Delete(Setting setting);
    }

    public class SettingRepository : ISettingRepository
    {
        private readonly ContestEntities _contestEntities;

        public SettingRepository(ContestEntities contestEntities)
        {
            _contestEntities = contestEntities;
        }

        public IQueryable<Setting> Settings
        {
            get { return _contestEntities.Settings; }
        }

        public Setting Save(Setting entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<Setting>().Add(entity);
            else
            {
                var setting = _contestEntities.Settings.Single(x => x.Id == entity.Id);
                _contestEntities.Entry(setting).CurrentValues.SetValues(entity);
                //_contestEntities.Set<Setting>().Attach(entity);
            }
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(Setting entity)
        {
            _contestEntities.Set<Setting>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
