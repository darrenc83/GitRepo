using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YPMMS.Domain.Service.Services;

namespace YPMMS.Display.Website.Controllers
{
    public class TerminalController : AuthBaseController
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }
        
        [Route("Terminal/GetTerminals")]
        [HttpGet]
        public async Task<ActionResult> GetTerminalsAsync()
        {
            try
            {
                var service = new TerminalDataService(Config.DbConnectionString);
                var terminals = await service.GetTerminalsAsync();
                return Json(terminals.ToList());
            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
            //return null;
        }






    }
}