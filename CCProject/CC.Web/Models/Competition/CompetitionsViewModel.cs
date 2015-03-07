using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Competition
{
    public class CompetitionsViewModel
    {
        public IEnumerable<CompetitionViewModel> CompetitionViewModels { get; set; }

        public CompetitionsViewModel(IEnumerable<Domain.Entities.Competition> competitions)
        {
                CompetitionViewModels = competitions.ToList().ConvertAll(c => new CompetitionViewModel(c));
        }
    }
}