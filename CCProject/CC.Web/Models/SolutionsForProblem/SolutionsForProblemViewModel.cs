using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Web.Models.Solution;

namespace CC.Web.Models.SolutionsForProblem
{
    public class SolutionsForProblemViewModel
    {
        public ListedSolutionViewModel TopSolution { get; set; }

        public string ShortName { get; set; }

        public int ProblemId { get; set; }

        public int Id { get; set; }

        public SolutionsForProblemViewModel(Domain.Entities.Solution solution, Domain.Entities.Problem problem)
        {
            TopSolution = new ListedSolutionViewModel(solution);
            ShortName = problem.ShortName;
            ProblemId = problem.Id;
        }
    }
}