using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using YPMMS.Display.Website.Attributes;
using YPMMS.Display.Website.Enums;
using YPMMS.Display.Website.Models.Admin;

using YPMMS.Domain.Service.Services;
using YPMMS.Shared.Core.Enums;
using YPMMS.Shared.Core.Models;
using Microsoft.AspNet.Identity;

namespace YPMMS.Display.Website.Controllers
{
    /// <summary>
    /// Controller for the Admin section of the site
    /// </summary>
    [AuthorizeRoles(Role.Admin, Role.Manager)]
    public class AdminController : AuthBaseController
    {
        #region View actions

        public ActionResult Index()
        {
            SessionHelper.ActiveSection = SiteSection.Admin;
            return View(CurrentUserDetails);
        }

        public ActionResult MerchantManagement()
        {
            SessionHelper.ActiveSection = SiteSection.Admin;          


            return View();
        }

        public ActionResult AddMerchant()
        {
            SessionHelper.ActiveSection = SiteSection.Admin;
            return View("AddEditMerchant");
        }

        [Route("Admin/EditMerchant/{merchantId}")]
        public ActionResult EditMerchant(long merchantId)
        {
            SessionHelper.ActiveSection = SiteSection.Admin;
            return View("AddEditMerchant", new EditSystemViewModel
            {
                MerchantId = merchantId
            });
        }

        [Route("Admin/Collector/Add")]
        public ActionResult AddCollector()
        {
            SessionHelper.ActiveSection = SiteSection.Admin;
            return View();
        }

        [Route("Admin/Collector")]
        public ActionResult Collector(long? machineId)
        {
            SessionHelper.ActiveSection = SiteSection.Admin;
            return View();
        }

        [Route("Admin/Collector/Profile")]
        public ActionResult ProfileAction(long? collectorId)
        {
            SessionHelper.ActiveSection = SiteSection.Admin;
            return View("Profile");
        }

        #endregion

        #region Data actions

        /// <summary>
        /// Get all machines
        /// </summary>
        [Route("Admin/MerchantData")]
        public async Task<ActionResult> MerchantDataAsync()
        {

            long ac = 0;
            string userName = string.Empty;
            string userId = string.Empty;
            bool isInAdminRole = false;
            IList<Merchant> merchants = new List<Merchant>();

            if (SessionHelper.CurrentUserDetails != null)
            {
                ac = SessionHelper.CurrentUserDetails.Account;
                userName = SessionHelper.CurrentUserDetails.UserName;
                userId = SessionHelper.CurrentUserDetails.Id;
            }
            //get role for user
            var userRoles = UserManager.GetRoles(userId);
            
            //if there are more than one role then loop through and see if they are in admin
            foreach (var role in userRoles)
            {
                if (role == "Admin")
                {
                    isInAdminRole = true;
                }

            }            

            try
            {

                var merchantService = new MerchantDataService(Config.DbConnectionString);

                //user is in admin role so get all machines
                if (isInAdminRole)
                {
                    merchants = await merchantService.GetAllMerchantsAsync();

                }
                //user isnt in admin role so only show get machines for their account


                //var sortedMachines = merchants
                //      .Select(Mapper.Map<AdminMachineJsonModel>)
                //      .OrderBy(m => m.Name)
                //      .ThenBy(m => m.Id);

                //return View("MerchantManagement",merchants);
                return Json(merchants);

            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
        }

        /// <summary>
        ///  Get all sites
        /// </summary>


        /// <summary>
        /// Add a new machine
        /// </summary>
        /// <param name="machineModel">The machine to add</param>
        //[Route("Admin/AddMachine")]
        //[HttpPost]
        //public async Task<ActionResult> AddMachineAsync(AdminMachineJsonModel machineModel)
        //{
        //    try
        //    {
        //        var machineService = new MerchantDataService(Config.DbConnectionString);
        //        var machine = Mapper.Map<Machine>(machineModel);
        //        if (SessionHelper.CurrentUserDetails != null)
        //        {
        //            machine.Account = SessionHelper.CurrentUserDetails.Account;
        //        }
        //        //Added this as a constant as this is a Tebs/SCC only system now.
        //        machine.SystemType = SystemType.CashInTebs;

        //        await machineService.AddMachineAsync(machine);

        //        var systemEventService = new SystemEventDataService(Config.DbConnectionString);
        //        await systemEventService.AddMachineAddedEventAsync(
        //            machine.Id,
        //            machine.SiteId,
        //            CurrentUserDetails.Id);

        //        SignalRController.OnSystemEventAdded();

        //        return new HttpStatusCodeResult(HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //        return InternalServerError();
        //    }
        //}

        /// <summary>
        /// Edit a new machine
        /// </summary>
        /// <param name="machineModel">The machine to edit</param>
        //[Route("Admin/EditMachine")]
        //[HttpPost]
        //public async Task<ActionResult> EditMachineAsync(AdminMachineJsonModel machineModel)
        //{
        //    try
        //    {
        //        // Get the old machine first, so we can detect if the site has changed
        //        var machineService = new MerchantDataService(Config.DbConnectionString);
        //        var oldMachine = await machineService.GetMachineAsync(machineModel.Id);

        //        var newMachine = Mapper.Map<Machine>(machineModel);
        //        await machineService.EditMachineAsync(newMachine);

        //        var systemEventService = new SystemEventDataService(Config.DbConnectionString);
        //        await systemEventService.AddMachineDetailsEditedEventAsync(
        //            newMachine.Id,
        //            CurrentUserDetails.Id,
        //            (newMachine.SiteId != oldMachine.SiteId) ? newMachine.SiteId : 0);

        //        SignalRController.OnSystemEventAdded();

        //        return new HttpStatusCodeResult(HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //        return InternalServerError();
        //    }
        //}

        /// <summary>
        /// Delete one or more machines
        /// </summary>
        /// <param name="model">Model specifying the machines to delete</param>
        /// <returns></returns>
        //[Route("Admin/DeleteMachines")]
        //[HttpPost]
        //public async Task<ActionResult> DeleteMachinesAsync(DeleteMachinesJsonModel model)
        //{
        //    try
        //    {
        //        if (!UserManager.CheckPassword(ApplicationUser, model.Password))
        //        {
        //            // jQuery doesn't pick up 401 (Unauthorized) errors properly :(
        //            return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        //        }

        //        var machineService = new MerchantDataService(Config.DbConnectionString);
        //        var deletedMachines = await machineService.DeleteMachinesAsync(model.MachineIds);

        //        var systemEventService = new SystemEventDataService(Config.DbConnectionString);
        //        await systemEventService.AddMachineRemovedEventsAsync(
        //            deletedMachines,
        //            CurrentUserDetails.Id);

        //        SignalRController.OnSystemEventAdded();

        //        return new HttpStatusCodeResult(HttpStatusCode.OK);
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //        return InternalServerError();
        //    }
        //}



        /// <summary>
        /// Check if the details entered for a new machine are valid
        /// </summary>
        /// <param name="machineId">Proposed machine ID</param>
        /// <param name="machineName">Proposed machine name</param>
        /// <param name="editMode">True if we're editing, false if we're adding</param>
        [Route("Admin/validateMerchantDetails")]
        [HttpGet]
        public async Task<ActionResult> ValidateMerchantDetailsAsync(string merchantId, string merchantName, bool editMode)
        {
            try
            {
                var resultModel = new ValidateMachineJsonModel();
                var merchantService = new MerchantDataService(Config.DbConnectionString);

                long merchantIdLong;
                if (!long.TryParse(merchantId, out merchantIdLong))
                {
                    resultModel.IdErrorMessage = "The Merchant ID must consist of digits only";
                }
                else if (!editMode && await merchantService.DoesMerchantExistAsync(merchantIdLong))
                {
                    resultModel.IdErrorMessage = "This Merchant already exists";
                }

                long? ignoreMerchantId = (editMode ? merchantIdLong : (long?)null);
                if (await merchantService.DoesMachineNameExistAsync(merchantName, ignoreMerchantId))
                {
                    resultModel.NameErrorMessage = "Sorry, this name is already in use";
                }

                return Json(resultModel);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
        }








        #endregion
    }
}