using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Team
{
    public class AddPersonViewModel
    {
        public int TeamId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public AddPersonViewModel()
        {
        }

        public AddPersonViewModel(int id, string userName)
        {
            TeamId = id;
            UserName = userName;
        }
    }
}