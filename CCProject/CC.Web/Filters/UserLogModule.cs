using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using System.Diagnostics;
using System.Web.Mvc;
using CC.Service;
using System.Web.Security;
using System.Web.Routing;
using CC.Domain.Repositories;
using CC.Domain.Entities;

namespace CC.Web.Filters
{
    public interface ILogActionFilter
    {
        void OnActionExecuting(ActionExecutingContext filterContext);
    }

        public class LogActionFilter : ActionFilterAttribute
        {
            public IPersonService PersonService { get; set; }

            public LogActionFilter()
            {
                //var context = new ContestEntities();
            }

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (PersonService.ByEmail(Membership.GetUser().Email) == null)
                {
                    RouteValueDictionary routeDictionary = routeDictionary = new RouteValueDictionary { { "action", "Index" }, { "controller", "CreateProfile" } };
                    filterContext.Result = new RedirectToRouteResult(routeDictionary);
                    //filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Route)
                }
                // Log("OnActionExecuting", filterContext.RouteData);
            }



            //public override void OnActionExecuted(ActionExecutedContext filterContext)
            //{
            //     Log("OnActionExecuted", filterContext.RouteData);       
            //}

            //public override void OnResultExecuting(ResultExecutingContext filterContext)
            //{
            //     Log("OnResultExecuting", filterContext.RouteData);       
            //}

            //public override void OnResultExecuted(ResultExecutedContext filterContext)
            //{
            //     Log("OnResultExecuted", filterContext.RouteData);       
            //}


            //private void Log(string methodName, RouteData routeData)
            //{
            //     var controllerName = routeData.Values["controller"];
            //     var actionName = routeData.Values["action"];
            //     var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
            //     Debug.WriteLine(message, "Action Filter Log");
            //}

        }
    }
