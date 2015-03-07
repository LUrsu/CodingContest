using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Person
{
    public class PeopleViewModel
    {
        public IEnumerable<ListedPersonViewModel> PersonViewModels { get; set; }

        public PeopleViewModel(IEnumerable<Domain.Entities.Person> people)
        {
            PersonViewModels = people.ToList().ConvertAll(p => new ListedPersonViewModel(p));
        }
    }
}