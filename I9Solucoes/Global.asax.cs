using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace I9Solucoes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["totalVisitantesOnline"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application["totalVisitantesOnline"] = (int)(Application["totalVisitantesOnline"]) + 1;
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            Application["totalVisitantesOnline"] = (int)(Application["totalVisitantesOnline"]) - 1;
        }

    }
}
