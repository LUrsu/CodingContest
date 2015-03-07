using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Solution
{
    public class SolutionsViewModel
    {
        public IEnumerable<SolutionViewModel> SolutionViewModels { get; set; }

        public SolutionsViewModel(IEnumerable<Domain.Entities.Solution> solutions)
        {
            SolutionViewModels = solutions.ToList().ConvertAll(s => new SolutionViewModel(s));
        }
    }
}