using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Competition
{
    public class CompetitionRegistrationViewModel
    {
        public CompetitionRegistrationViewModel(Domain.Entities.Competition competition, List<Domain.Entities.Team> personsTeams)
        {
            CompetitionName = competition.Name;
            Id = competition.Id;
            PersonsTeams = personsTeams;
        }

        public CompetitionRegistrationViewModel()
        {
        }

        public string CompetitionName { get; set; }

        public int Id { get; set; }

        [Required]
        public string PassWord { get; set; }

        [Required]
        public int TeamId { get; set; }

        public List<Domain.Entities.Team> PersonsTeams { get; set; }
    }
}