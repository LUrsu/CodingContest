using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Competition
{
    public class CompetitionFormViewModel
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int SettingId { get; set; }

        public CompetitionFormViewModel(int personId, int settingId)
        {
            AdminId = personId;
            SettingId = settingId;
        }

        public CompetitionFormViewModel()
        {
        }
    }
}