using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC_EF_Temple
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //查看数据库表
            //routes.MapRoute(
            //    name: "home",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //    //namespaces: new string[] { "MVC_EF_Temple.Controllers" }
            //);

            // routes.MapRoute(
            //   name: "Login",
            //   url: "Login",
            //   defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            //);

            //查看数据库表
            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "SqlServerSelect", action = "Index", id = UrlParameter.Optional }
            //namespaces: new string[] { "MVC_EF_Temple.Controllers" }
            );
        }
    }
}