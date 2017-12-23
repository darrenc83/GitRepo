using System.Web;
using System.Web.SessionState;
using AutoMapper;
using YPMMS.Display.Website.Enums;
using YPMMS.Display.Website.IdentityConfig;
using YPMMS.Display.Website.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace YPMMS.Display.Website
{
    /// <summary>
    /// Helper class for getting and setting session values. This class should always be used instead of
    /// bare <code>Session["Key"]</code> style access.
    /// </summary>
    public static class SessionHelper
    {
        private static HttpSessionState _session;

        private const string CurrentUserDetailsKey = "CurrentUserDetails";
        private const string ActiveSectionKey = "ActiveSection";

        /// <summary>
        /// The currently active section of the site
        /// </summary>
        public static SiteSection? ActiveSection
        {
            get
            {
                _session = HttpContext.Current.Session;
                return (SiteSection?)_session[ActiveSectionKey];
            }
            set
            {
                _session = HttpContext.Current.Session;
                _session[ActiveSectionKey] = value;
            }
        }

        /// <summary>
        /// Basic details about the currently logged-in user
        /// </summary>
        public static CurrentUserDetailsModel CurrentUserDetails
        {
            get
            {
                _session = HttpContext.Current.Session;
                var details = (CurrentUserDetailsModel)_session[CurrentUserDetailsKey];
                //if (details == null)
                //{
                //    //var applicationUser = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());
                //    //details = Mapper.Map<CurrentUserDetailsModel>(applicationUser);
                //    details = new LiveCollect.Display.Website.Models.Account.CurrentUserDetailsModel() { Name = "Session has timed out", Email = "Please Logout", Account=999999999 };
                //    _session[CurrentUserDetailsKey] = details;
                //}
                return details;
            }
            set
            {
                _session = HttpContext.Current.Session;
                _session[CurrentUserDetailsKey] = value;
            }
        }
    }
}