using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Problem
{
    public class DisplaySolutionsViewModel
    {
        public ProblemViewModel Problem { get; set; }
        public Domain.Entities.Team Team { get; set; }
        public IEnumerable<Domain.Entities.Solution> Solutions { get; set; }
    }
}