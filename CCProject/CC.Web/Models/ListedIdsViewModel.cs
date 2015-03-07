using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CC.Web.Models
{
    public class ListedIdsViewModel
    {
        public List<int> Ids { get; set; }

        public ListedIdsViewModel(List<int> ids)
        {
            Ids = ids;
        }
    }
}