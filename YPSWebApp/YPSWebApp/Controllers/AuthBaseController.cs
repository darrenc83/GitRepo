using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using YPSWebApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;

namespace YPSWebApp.Controllers
{
    [Authorize]
    public abstract class AuthBaseController :Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
     

        protected AuthBaseController()
        {
        }

        public AuthBaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        protected new JsonResult Json(object data)
        {
            return new CustomJsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        private class CustomJsonResult : JsonResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                if (Data == null)
                {
                    return;
                }

                var response = context.HttpContext.Response;

                response.ContentType = "application/json";

                if (ContentEncoding != null)
                {
                    response.ContentEncoding = ContentEncoding;
                }

                response.Write(JsonConvert.SerializeObject(Data, JsonSettings));
            }
        }
        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            // Format dates as ISO strings (e.g. "2016-07-27T10:00:00Z") rather than the bizarre default Microsoft format
            // (See http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_DateFormatHandling.htm)
            DateFormatHandling = DateFormatHandling.IsoDateFormat,

            // Convert enum values to strings rather than integers
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter()
            }
        };
        /// <summary>
        /// Get an <see cref="ApplicationUser"/> object representing the current user.
        /// Note that where possible, you should call <see cref="AuthBaseController.CurrentUserDetails"/> instead
        /// (e.g. where you just need the user's ID or name) to avoid the potential overhead of calling
        /// <see cref="UserManager.FindById()"/> in this getter.
        /// </summary>


        /// <summary>
        /// Wrapper property on <see cref="SessionHelper.CurrentUserDetails"/> to get the basic details
        /// of the current user (ID, name, e-mail address, etc.) straight from session. This is faster than
        /// using the <see cref="AuthBaseController.ApplicationUser"/> property and should be used instead
        /// of it where possible.
        /// </summary>
        //protected CurrentUserDetailsModel CurrentUserDetails
        //{
        //    get
        //    {
        //        return SessionHelper.CurrentUserDetails;
        //    }
        //    //set
        //    //{
        //    //    SessionHelper.CurrentUserDetails = value;
        //    //}
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}