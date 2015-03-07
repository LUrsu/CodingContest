using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CC.Service;
using CC.Web.Binders;
using CC.Web.Models;

namespace CC.Web.Controllers
{
    public class AccountController : Controller
    {
        public AuthProvider authProvider { get; set; }
        public IPersonService PersonService { get; set; }

        public AccountController(IPersonService personService)
        {
            authProvider = new AuthProvider();
            PersonService = personService;
        }

        public PartialViewResult LoginPartial()
        {
            var loginView = new LogOnViewModel();
            return PartialView("LoginPartial", loginView);
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {

            if (returnUrl == "")
                returnUrl = null;
            if (authProvider.Authenticate(model.UserName, model.Password))
            {
                UserSession.LoggedInUser = PersonService.ByUserName(model.UserName);
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            }
            return View();
        }

        public ActionResult Logout()
        {
            UserSession.LoggedInUser = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }

    public class AuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            var result = Membership.ValidateUser(username, password);

            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }

            return result;
        }
    }
}
