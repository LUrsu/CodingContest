using System;
using System.Collections.Generic;

namespace CC.Domain.CodeCompilers
{
    public class SolutionModel
    {
        public int ProblemId { get; set; }
        public int TeamId { get; set; }
        public string FileName { get; set; }
        public string ProblemShortName { get; set; }
        public DateTime SubmissionTime { get; set; }
        public DateTime JudgeTime { get; set; }
        public string Result { get; set; }
        public string ResultDescription { get; set; }
        public string SourceCode { get; set; }
        public string GeneratedOutput { get; set; }
        public string ActualInput { get; set; }
        public string ExpectedOutput { get; set; }
        public int SubmissionTimeout { get; set; }
        public int TimesAttemped { get; set; }
        public int PenaltyAmount { get; set; }
        public int Score { get; set; }
        public DateTime CompetitionStartTime { get; set; }
    }
}
