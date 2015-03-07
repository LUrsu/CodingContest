using System.ComponentModel.DataAnnotations;
using CC.Domain.Entities;
using System.Linq;

namespace CC.Web.Models.Person
{
    public class PersonViewModel
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string FullName { get; set; }

        [ScaffoldColumn(false)]
        public bool IsLoggedInPerson { get; set; }

        public bool HasTeams { get; set; }

        public bool InCompetitions { get; set; }

        public PersonViewModel(Domain.Entities.Person person, bool isLoggedInPerson = false)
        {
            Id = person.Id;
            Email = person.Email;
            FirstName = person.FirstName;
            LastName = person.LastName;
            UserName = person.UserName;
            FullName = FirstName + " " + LastName;
            HasTeams = person.Teams.Count > 0;
            IsLoggedInPerson = isLoggedInPerson;
            InCompetitions = person.Teams.SelectMany(t => t.TeamInCompetitions).Any();
        }
    }
}