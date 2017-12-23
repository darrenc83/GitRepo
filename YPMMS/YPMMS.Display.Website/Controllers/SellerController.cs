using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YPMMS.Domain.Service.Services;

namespace YPMMS.Display.Website.Controllers
{
    public class SellerController : AuthBaseController
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }


        [Route("Seller/GetSellers")]
        [HttpGet]
        public async Task<ActionResult> GetSellersAsync()
        {
            try
            {
                var service = new SellerDataService(Config.DbConnectionString);
                var sellers = await service.GetSellersAsync();
                return Json(sellers.ToList());
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