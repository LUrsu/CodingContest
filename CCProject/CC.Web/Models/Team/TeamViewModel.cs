using System.ComponentModel.DataAnnotations;

namespace CC.Web.Models.Team
{
    public class TeamViewModel
    {
        [Required]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public bool HasCompetition { get; set; }

        [ScaffoldColumn(false)]
        public string CompetitionName { get; set; }

        [ScaffoldColumn(false)]
        public int CompetitionId { get; set; }

        public bool IsOnTeam { get; set; }

        public string Password { get; set; }

        public Domain.Entities.Competition Competition { get; set; }

        public TeamViewModel(Domain.Entities.Team team, Domain.Entities.Competition competition, bool onTeam)
        {
            Name = team.Name;
            Id = team.Id;
            HasCompetition = competition != null;
            if (!HasCompetition) return;
            Competition = competition;
            CompetitionName = competition.Name;
            CompetitionId = competition.Id;
            IsOnTeam = onTeam;
        }

        public TeamViewModel()
        {
        }
    }
}