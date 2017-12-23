using System.Web.Mvc;

namespace YPMMS.Display.Website.Controllers
{
    /// <summary>
    /// Unauthorized controller for the site root page
    /// </summary>
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            Response.Redirect("/Account/Login");
            return View();
        }
    }
}