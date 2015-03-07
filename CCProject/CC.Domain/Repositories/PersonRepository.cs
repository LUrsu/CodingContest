using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CC.Domain.Entities;

namespace CC.Domain.Repositories
{
    public interface IPersonRepository
    {
        IQueryable<Person> People { get; }
        Person Save(Person person);
        void Delete(Person person);
    }

    public class PersonRepository : IPersonRepository
    {

        private readonly ContestEntities _contestEntities;

        public PersonRepository(ContestEntities contestEntities)
        {
            _contestEntities = contestEntities;
        }

        public IQueryable<Person> People
        {
            get { return _contestEntities.People; }
        }

        public Person Save(Person entity)
        {
            var validationErrors = _contestEntities.GetValidationErrors().ToList();
            if (validationErrors.Count > 0)
                throw new ValidateException(validationErrors);

            if (_contestEntities.Entry(entity).State == EntityState.Detached)
                _contestEntities.Set<Person>().Add(entity);
            else
                _contestEntities.Set<Person>().Attach(entity);
            _contestEntities.SaveChanges();
            return entity;
        }

        public void Delete(Person entity)
        {
            _contestEntities.Set<Person>().Remove(entity);
            _contestEntities.SaveChanges();
        }
    }
}
