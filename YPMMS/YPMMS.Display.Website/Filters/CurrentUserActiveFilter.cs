using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YPMMS.Display.Website.Filters
{
    public class CurrentUserActiveFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionHelper.CurrentUserDetails == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "action", "LogIn" },
                    { "controller", "Account" },
                    { "returnUrl", filterContext.HttpContext.Request.RawUrl}
                });
            }
        }
    }
} 