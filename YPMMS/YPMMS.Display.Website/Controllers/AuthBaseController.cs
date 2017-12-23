using System.Web;
using System.Web.Mvc;
using YPMMS.Display.Website.IdentityConfig;
using YPMMS.Display.Website.Models.Account;
using YPMMS.Display.Website.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace YPMMS.Display.Website.Controllers
{
    /// <summary>
    /// Base class for all authorized controllers across the site.
    /// </summary>
    [Authorize]
    public abstract class AuthBaseController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationUser _applicationUser;

        protected AuthBaseController()
        {
        }

        protected AuthBaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            : this()
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected ApplicationSignInManager SignInManager
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

        protected ApplicationUserManager UserManager
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

        /// <summary>
        /// Get an <see cref="ApplicationUser"/> object representing the current user.
        /// Note that where possible, you should call <see cref="AuthBaseController.CurrentUserDetails"/> instead
        /// (e.g. where you just need the user's ID or name) to avoid the potential overhead of calling
        /// <see cref="UserManager.FindById()"/> in this getter.
        /// </summary>
        protected ApplicationUser ApplicationUser
        {
            get
            {
                _applicationUser = _applicationUser ?? UserManager.FindById(User.Identity.GetUserId());
                return _applicationUser;
            }
            set
            {
                _applicationUser = value;
            }
        }

        /// <summary>
        /// Wrapper property on <see cref="SessionHelper.CurrentUserDetails"/> to get the basic details
        /// of the current user (ID, name, e-mail address, etc.) straight from session. This is faster than
        /// using the <see cref="AuthBaseController.ApplicationUser"/> property and should be used instead
        /// of it where possible.
        /// </summary>
        protected CurrentUserDetailsModel CurrentUserDetails
        {
            get
            {
                return SessionHelper.CurrentUserDetails;
            }
            set
            {
                SessionHelper.CurrentUserDetails = value;
            }
        }

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