using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using YPMMS.Display.Utilities;
using YPMMS.Display.Website.Models.SystemEvents;
using YPMMS.Domain.Service.Services;

namespace YPMMS.Display.Website.Controllers
{
    public class SystemEventsController : AuthBaseController
    {
        #region Data actions

        /// <summary>
        /// Get system events for the current user
        /// </summary>
        /// <returns></returns>
        [Route("SystemEvents/GetEvents")]
        [HttpGet]
        public async Task<ActionResult> GetSystemEventsAsync(
            long beforeId = 0,
            DateTimeOffset? from = null,
            DateTimeOffset? to = null)
        {
            try
            {
                var systemEventService = new SystemEventDataService(Config.DbConnectionString);
                var events = await systemEventService.GetSystemEventsForUserAsync(
                    CurrentUserDetails.Id,
                    Consts.SystemEventsPageSize,
                    beforeId,
                    from,
                    to);

                return Json(new SystemEventsJsonModel(events));
            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get the latest system events from the database, to populate the header dropdown.
        /// </summary>
        /// <returns></returns>
        [Route("SystemEvents/GetLatestEvents")]
        [HttpGet]
        public async Task<ActionResult> GetLatestSystemEventsAsync()
        {
            try
            {
                var systemEventService = new SystemEventDataService(Config.DbConnectionString);
                var events = await systemEventService.GetLatestSystemEventsForUserAsync(
                    CurrentUserDetails.Id,
                    Consts.SystemEventsPageSize);
                return Json(new SystemEventsJsonModel(events));
            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get system events for a specific machine from the database
        /// </summary>
        /// <returns></returns>
        [Route("SystemEvents/GetEventsForMachine")]
        [HttpGet]
        public async Task<ActionResult> GetSystemEventsForMachineAsync(
            long machineId,
            long beforeId = 0,
            DateTimeOffset? from = null,
            DateTimeOffset? to = null)
        {
            try
            {
                var systemEventService = new SystemEventDataService(Config.DbConnectionString);
                var events = await systemEventService.GetSystemEventsForMachineAsync(
                    machineId,
                    CurrentUserDetails.Id,
                    Consts.SystemEventsPageSize,
                    beforeId,
                    from,
                    to);

                return Json(new SystemEventsJsonModel(events));
            }
            catch (Exception e)
            {
                Log.Error(e);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Mark all events before the current time as viewed by the current user
        /// </summary>
        /// <returns></returns>
        [Route("SystemEvents/MarkViewed")]
        [HttpPost]
        public async Task<ActionResult> MarkSystemEventsViewedAsync()
        {
            try
            {
                ApplicationUser.LastViewedEventsTime = DateTimeOffset.UtcNow;
                await UserManager.UpdateAsync(ApplicationUser);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
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