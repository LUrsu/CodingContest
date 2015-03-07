using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Solution
{
    public class ListedSolutionViewModel
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public DateTime SubmissionTime { get; set; }

        public ListedSolutionViewModel(Domain.Entities.Solution input)
        {
            Id = input.Id;
            Score = input.Score ?? 0;
            SubmissionTime = input.SubmissionTime;
        }
    }
}