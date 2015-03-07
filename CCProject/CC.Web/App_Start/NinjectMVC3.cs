using System.Web.Mvc;
using CC.Domain.Entities;
using CC.Domain.Repositories;
using CC.Service;
using CC.Web.Binders;
using Ninject.Web.Mvc.FilterBindingSyntax;

[assembly: WebActivator.PreApplicationStartMethod(typeof(CC.Web.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(CC.Web.App_Start.NinjectMVC3), "Stop")]

namespace CC.Web.App_Start
{
    using System.Reflection;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ContestEntities>().ToProvider<ContextFactory>().InRequestScope();

            kernel.BindFilter<ProfileFilter>(FilterScope.Global,0);

            kernel.Bind<IPersonRepository>().To<PersonRepository>();
            kernel.Bind<IPersonService>().To<PersonService>();

            kernel.Bind<IProblemRepository>().To<ProblemRepository>();
            kernel.Bind<IProblemService>().To<ProblemService>();

            kernel.Bind<ICompetitionRepository>().To<CompetitionRepository>();
            kernel.Bind<ICompetitionService>().To<CompetitionService>();

            kernel.Bind<ISolutionRepository>().To<SolutionRepository>();
            kernel.Bind<ISolutionService>().To<SolutionService>();

            kernel.Bind<ISettingRepository>().To<SettingRepository>();

            kernel.Bind<ITeamRepository>().To<TeamRepository>();
            kernel.Bind<ITeamService>().To<TeamService>();

            kernel.Bind<ITeamInCompetitionRepository>().To<TeamInCompetitionRepository>();
            kernel.Bind<ITeamInCompetitionService>().To<TeamInCompetitionService>();

            /**
            kernel.Bind<ISolutionsForProblemRepository>().To<SolutionsForProblemRepository>();
            kernel.Bind<ISolutionsForProblemService>().To<SolutionsForProblemService>();
             */

        }
    }
}
