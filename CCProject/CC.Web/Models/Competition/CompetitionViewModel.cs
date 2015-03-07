using System.Collections.Generic;
using CC.Web.Models.Problem;

namespace CC.Web.Models.Competition
{

    public class CompetitionViewModel
    {
        public Domain.Entities.Competition Competition { get; set; }

        public ProblemsViewModel Problems { get; set; }

        public Domain.Entities.Person Admin { get; set; }

        public IEnumerable<Domain.Entities.Team> RegisteredTeams { get; set; }

        public bool IsPersonRegistered { get; set; }

        public bool IsOngoing { get; set; }

        public CompetitionViewModel(Domain.Entities.Competition competition)
        {
            Competition = competition;
            Problems = new ProblemsViewModel(competition.Problems);
        }

    }

    /**
    public class CompetitionViewModel
    {
        [Required]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int AdminId { get; set; }
        
        public string AdminUserName { get; set; }
        
        public SettingViewModel Setting { get; set; }

        public bool IsOnGoing { get; set; }

        public bool InCompetition { get; set; }

        public ProblemsViewModel Problems { get; set; }

        public CompetitionViewModel(Domain.Entities.Competition competition,bool isOnGoing, bool visiterRegistered)
        {
            Name = competition.Name;
            Id = competition.Id;
            AdminId = competition.AdminId;
            Setting = new SettingViewModel(competition.Setting);
            IsOnGoing = isOnGoing;
            InCompetition = visiterRegistered;
            Problems = new ProblemsViewModel(competition.Problems);
        }

        public CompetitionViewModel(string admin)
        {
            AdminUserName = admin;
        }

        public CompetitionViewModel()
        {
            
        }

        public CompetitionViewModel(Domain.Entities.Competition competition)
        {
            Name = competition.Name;
            Id = competition.Id;
            AdminId = competition.AdminId;
            Setting = new SettingViewModel(competition.Setting);
        }
    }
     */
}