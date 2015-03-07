using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Problem
{
    public class ProblemsViewModel
    {
        public IEnumerable<ProblemViewModel> ProblemViewModels { get; set; }

        public ProblemsViewModel(IEnumerable<Domain.Entities.Problem> problems)
        {
            ProblemViewModels = problems.ToList().ConvertAll(c => new ProblemViewModel(c));
        }

    }
}