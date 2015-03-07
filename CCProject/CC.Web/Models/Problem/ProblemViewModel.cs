using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Web.Models.Competition;
using System.ComponentModel.DataAnnotations;

namespace CC.Web.Models.Problem
{
    public class ProblemViewModel
    {
        [ScaffoldColumn(false)]
        public int CompetitionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortName { get; set; }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ExampleInput { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ExampleOutput { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ActualInput { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string ExpectedOutput { get; set; }

        [ScaffoldColumn(false)]
        public Domain.Entities.Competition Competition { get; set; }

        public ProblemViewModel(Domain.Entities.Problem problem)
        {
            Id = problem.Id;
            Name = problem.Name;
            ShortName = problem.ShortName;
            Competition = problem.Competition;
            Description = problem.Description;
            ExampleInput = problem.ExampleInput;
            ExampleOutput = problem.ExampleOutput;
            ExpectedOutput = problem.ExpectedOutput;
            ActualInput = problem.ActualInput;
            CompetitionId = problem.CompetitionId;
        }

        public ProblemViewModel(int competitionId)
        {
            CompetitionId = competitionId;
        }

        public ProblemViewModel()
        {

        }
    }
}