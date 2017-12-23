using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using YPMMS.Display.Website.Enums;
using YPMMS.Display.Website.Models.Overview;
using YPMMS.Domain.Service.Services;
using YPMMS.Shared.Core.Models;
using YPMMS.Shared.Core.Enums;
using YPMMS.Display.Website.Filters;

namespace YPMMS.Display.Website.Controllers
{
    /// <summary>
    /// Controller for the machine overview page
    /// </summary>
    ///
    [Authorize(Roles = nameof(Role.Manager) + "," + nameof(Role.Admin))]
    public class OverviewController : AuthBaseController
    {
        #region View actions

        // GET: Overview
        [CurrentUserActiveFilter]
        public ActionResult Index()
        {
            SessionHelper.ActiveSection = SiteSection.Overview;

            return View();
        }

        #endregion

        #region Data actions

        [Route("Overview/MerchantData")]
        [CurrentUserActiveFilter]
        public async Task<ActionResult> MerchantDataAsync()
        {

            try
            {
                long ac = 0;
                if (SessionHelper.CurrentUserDetails != null)
                {
                    ac = SessionHelper.CurrentUserDetails.Account;
                }

                var service = new MerchantDataService(Config.DbConnectionString);
                var model = new OverviewMerchantsJsonModel
                {

                    Merchants = await service.GetAllMerchantsAsync()
                };

                return Json(model);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
        }


        //[Route("Overview/OverallData")]
        //public async Task<ActionResult> OverallDataAsync()
        //{
        //    try
        //    {

        //        long ac = 0;
        //        if (SessionHelper.CurrentUserDetails != null)
        //        {
        //            ac = SessionHelper.CurrentUserDetails.Account;
        //        }

        //        var service = new MerchantDataService(Config.DbConnectionString);
        //        //OverallTotal totals = await service.GetOverallDataAsync(0,ac);
               
        //        return Json(totals);
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //        return InternalServerError();
        //    }
        //}


        //[Route("Overview/Bag/{machineId}")]
        //[HttpGet]
        //public async Task<ActionResult> GetTebsBag(long machineId)
        //{
        //    try
        //    {
        //        var service = new MerchantDataService(Config.DbConnectionString);
        //        var tebsbag = await service.GetMachineBagAsync(machineId);
        //        return Json(tebsbag);
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //        return InternalServerError();
        //    }
        //}


        #endregion



    }
}