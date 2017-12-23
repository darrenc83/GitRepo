using YPMMS.Display.Website.Enums;
using YPMMS.Domain.Service.Services;
using YPMMS.Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YPMMS.Display.Website.Controllers.Report
{
    [RoutePrefix("Report")]
    public class RootController : BaseController
    {
        [Route("")]
        public ActionResult Index(int? year)
        {
            SessionHelper.ActiveSection = SiteSection.Reports;

            return View("~/Views/Report/Root/Index.cshtml");
        }
    }
}