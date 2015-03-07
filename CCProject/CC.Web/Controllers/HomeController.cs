using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC.Domain.CodeCompilers;
using CC.Web.Models.Solution;
using System.Web.Security;
using CC.Web.Models.Competition;
using CC.Service;

namespace CC.Web.Controllers
{
    
    public class HomeController : Controller
    {
        public ICompetitionService Service { get; set; }

        public HomeController(ICompetitionService service)
        {
            Service = service;
        }

        public ActionResult Dumb()
        {
            return Redirect("http://my.neumont.edu/nuid/service.aspx?ReturnUrl=http://localhost:18845/");
        }
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SolutionForm()
        {
            var SolutionForm = new SolutionFormViewModel();
            return PartialView("SolutionFormPartial", SolutionForm);
        }

        public ActionResult SubmitSolution(HttpPostedFileBase Solution, string username)
        {
            Solution.SaveAs("C:\\" + Solution.FileName);
            JavaCompiler.CompileJavaFile(@"C:\m", "C:\\" + Solution.FileName);
            return View("Index");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
