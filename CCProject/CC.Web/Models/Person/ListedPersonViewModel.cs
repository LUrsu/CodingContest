using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CC.Domain.Entities;

namespace CC.Web.Models.Person
{
    public class ListedPersonViewModel
    {
        public string FullName { get; set; }
        public int Id { get; set; }

        public ListedPersonViewModel(Domain.Entities.Person person)
        {
            Id = person.Id;
            FullName = person.FirstName + " " + person.LastName;
        }
    }
}