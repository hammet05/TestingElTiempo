using Autofac.Integration.Mvc;
using JobBoard.Data;
using JobBoard.Web.App_Start;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JobBoard.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = AutofacConfig.RegisterDependencies();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
