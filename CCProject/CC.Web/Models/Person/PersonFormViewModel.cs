using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CC.Web.Models.Person
{
    public class PersonFormViewModel
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string UserName { get; set; }

        public PersonFormViewModel(string userName)
        {
            UserName = userName;
        }

        public PersonFormViewModel()
        {

        }
    }
}