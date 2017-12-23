using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using YPMMS.Domain.Service.Services;
using YPMMS.Shared.Core.Models;
using YPMMS.Shared.Core.Enums;
using System.Net;

namespace YPMMS.Display.Website.Controllers
{
    /// <summary>
    /// Controller for machine actions
    /// </summary>
    [Authorize(Roles = nameof(Role.Manager) + "," + nameof(Role.Admin))]
    public class MerchantController : AuthBaseController
    {
        #region View actions

        [Route("Machine/{machineId}")]
        public async Task<ActionResult> Index(long machineId)
        {
            //var service = new MerchantDataService(Config.DbConnectionString);
            //var machine = await service.GetMachineWithCollectionsAsync(machineId);
            //if (machine.SystemType.ToString() == "CashInTebs" || machine.SystemType.ToString() == "CashInBnv")
            //{
            //    var tebsbag = await service.GetMachineBagAsync(machineId);
            //    machine.CurrentBagData = tebsbag;
            //}
            //if (machine == null)
            //{
            //    return View("Error");
            //}

            return View();
        }

        [Route("Machine/{machineId}/Events")]
        public async Task<ActionResult> Events(long machineId)
        {
            //var service = new MerchantDataService(Config.DbConnectionString);
            //var machine = await service.GetMachineAsync(machineId);
            //if (machine == null)
            //{
            //    return View("Error");
            //}

            //return View(machine);
            return View();
        }


        [Route("Machine/TebsBag/{tebsCode}")]
        public async Task<ActionResult> TebsBag(string tebsCode)
        {
            //var service = new MerchantDataService(Config.DbConnectionString);
            //var bag = await service.GetTebsBagAsync(tebsCode);
            //if (bag == null)
            //{
            //    return View("Error");
            //}

            return View();
        }


        #endregion

        #region Data actions

       


        [Route("Merchant/GetMerchant/{merchantId}")]
        [HttpGet]
        public async Task<ActionResult> GetMerchantAsync(long merchantId)
        {
            try
            {
                var service = new MerchantDataService(Config.DbConnectionString);
                var merchant = await service.GetMerchantAsync(merchantId);
                return Json(merchant);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
            //return null;
        }


      



        #endregion
    }
}