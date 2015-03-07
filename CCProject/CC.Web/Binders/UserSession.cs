using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using CC.Domain.Entities;

namespace CC.Web.Binders
{
    public class UserSession
    {
        private const string sessionKey = "LoggedInUser";

        public static Person LoggedInUser
        {
            get
            {
                if (Membership.GetUser() == null)
                    HttpContext.Current.Session[sessionKey] = null;
                return (Person)HttpContext.Current.Session[sessionKey];
            }
            set { HttpContext.Current.Session[sessionKey] = value; }
        }
    }
}