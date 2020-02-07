using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WriterPlatformApp.WEB.App_Start;

namespace WriterPlatformApp.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            NinjectConfig.Configure();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
