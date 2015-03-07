using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CC.Web.Models.Problem;

namespace CC.Web.Models.Competition
{
    public class CompetitionEditViewModel
    {
        [Required]
        public Domain.Entities.Competition Competition { get; set; }

        public ProblemsViewModel Problems { get; set; }

        public Domain.Entities.Person Admin { get; set; }

        public IEnumerable<Domain.Entities.Team> RegisteredTeams { get; set; }

        public CompetitionEditViewModel(Domain.Entities.Competition competition)
        {
            Competition = competition;
            Problems = new ProblemsViewModel(competition.Problems);
        }

        public CompetitionEditViewModel()
        {
            
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int AdminId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        [ScaffoldColumn(false)]
        public int CompetitionId { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [ScaffoldColumn(false)]
        public int SettingId { get; set; }

        private int _penaltyAmount = 20;
        [Required]
        public int PenaltyAmount { get { return _penaltyAmount; } set { _penaltyAmount = value; } }

        [Required]
        public DateTime StartTime { get; set; }

        private int _submissionTimeout = 5;
        [Required]
        public int SubmissionTimeout { get { return _submissionTimeout; } set { _submissionTimeout = value; } }
    }
}