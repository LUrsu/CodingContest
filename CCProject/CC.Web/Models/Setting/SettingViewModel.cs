using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CC.Web.Models.Setting
{
    public class SettingViewModel
    {
        public Domain.Entities.Competition Competition { get; set; }

        [ScaffoldColumn(false)]
        public int CompetitionId { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public int PentaltyAmount { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public int SubmissionTimeLimit { get; set; }

        public SettingViewModel(int competitionId)
        {
            CompetitionId = competitionId;
        }

        public SettingViewModel()
        {
        }

        public SettingViewModel(CC.Domain.Entities.Setting setting)
        {
            EndTime = setting.EndTime;
            Id = setting.Id;
            PentaltyAmount = setting.PentaltyAmount;
            StartTime = setting.StartTime;
            SubmissionTimeLimit = setting.SubmissionTimeLimit;
        }
    }
}
