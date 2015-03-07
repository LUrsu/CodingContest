using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CC.Web.Models;

namespace CC.Web.Controllers.Dummy
{
    public class AddFakeStudentController : Controller
    {
        //
        // GET: /AddFakeStudent/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(AddFakeStudentViewModel model)
        {
            try
            {
                Membership.CreateUser(model.UserName, model.Password);
            }
            catch
            {
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
