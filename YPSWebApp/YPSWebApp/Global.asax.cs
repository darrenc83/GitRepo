using AutoMapper;
using Shared.Core.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YPSWebApp.App_Start;
using YPSWebApp.Models.Merchant;

namespace YPSWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.RegisterMaps();

        }

        protected void Application_End()
        {
            
           
        }
    }
}
