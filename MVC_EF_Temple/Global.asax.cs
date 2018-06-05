using MVC_EF_Temple.Automapper;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_EF_Temple
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AutoMapper映射初始化
            RegisterAutomapper.Excute();
        }
    }
}