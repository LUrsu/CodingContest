using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using CC.Domain.Entities;
using CC.Service;
using CC.Web.App_Start;
using Ninject;

namespace CC.Web.Binders
{
    public class ProfileFilter : IActionFilter
    {
        public IPersonService Service { get; set; }

        public ProfileFilter(IPersonService service)
        {
            Service = service;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Membership.GetUser() == null)
            {
                UserSession.LoggedInUser = null;
                return;
            }

            if (UserSession.LoggedInUser != null) return;
            
            if (Service.ByUserName(Membership.GetUser().UserName) == null)
            {
                if (filterContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString() !=
                    "Person")
                {
                    if (filterContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString() !=
                        "Account")
                    {
                        var routeDictionary = new RouteValueDictionary
                                                  {
                                                      {"action", "Index"},
                                                      {"controller", "Person"},
                                                      {"username", Membership.GetUser().UserName}
                                                  };
                        filterContext.Result = new RedirectToRouteResult(routeDictionary);
                    }
                }
            }
            else
            {
                UserSession.LoggedInUser = Service.ByUserName(Membership.GetUser().UserName);
            }
        }
    }
}