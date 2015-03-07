using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Solution
{
    public class SolutionViewModel
    {

    

        public int Id { get; set; }

        public int ProblemId { get; set; }

        public int Score { get; set; }

        public Domain.Entities.Team Team { get; set; }

        /**
        [ScaffoldColumn(false)]
        public Domain.Entities.SolutionsForProblem SolutionsForProblem { get; set; }
         */

        [ScaffoldColumn(false)]
        public File Source { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Time { get; set; }

        public string Result { get; set; }

        [ScaffoldColumn(false)]
        public int SolutionsForProblemID { get; set; }

        public SolutionViewModel(Domain.Entities.Solution solution) 
        {
            Id = solution.Id;
            ProblemId = solution.ProblemId;
            Team = solution.Team;
            Source = solution.SourceFile;
            Score = solution.Score ?? 0;
            /**
            SolutionsForProblemID = solution.SolutionsForProblemId;
             */
            Result = solution.Result;
            Time = solution.SubmissionTime;
        }
    }
}