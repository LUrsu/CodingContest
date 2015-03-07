using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CC.Domain.Entities;
using CC.Domain.Repositories;

namespace CC.Service
{
    public interface IPersonService
    {
        Person ById(int id);
        Team ByTeamId(int id);
        Person ByEmail(string email);
        Person ByUserName(string userName);
        IEnumerable<Person> AllUsersByUserName(String userName);
        Person Save(Person person);
        Team SaveTeam(Team team);
        IEnumerable<Team> TeamsIn(int personId);
    }

    public class PersonService : IPersonService
    {

        private IPersonRepository PersonRepository { get; set; }
        public ITeamRepository TeamRepository { get; set; }

        public PersonService(IPersonRepository personRepository, ITeamRepository teamRepository)
        {
            PersonRepository = personRepository;
            TeamRepository = teamRepository;
        }

        public Person ById(int id)
        {
            return PersonRepository.People.FirstOrDefault(p => p.Id == id);
        }

        public Team ByTeamId(int id)
        {
            return TeamRepository.Teams.FirstOrDefault(t => t.Id == id);
        }

        public Person ByEmail(string email)
        {
            return PersonRepository.People.FirstOrDefault(p => p.Email == email);
        }

        public Person ByUserName(string userName)
        {
            return PersonRepository.People.FirstOrDefault(p => p.UserName == userName);
        }

        public Person Save(Person person)
        {
            return PersonRepository.Save(person);
        }

        public Team SaveTeam(Team team)
        {
            return TeamRepository.Save(team);
        }

        public IEnumerable<Person> AllUsersByUserName(string userName)
        {
            return PersonRepository.People.Where(p => p.UserName == userName);
        }

        public IEnumerable<Team> TeamsIn(int personId)
        {
            var person = ById(personId);

            return person.Teams;
        }
    }
}
