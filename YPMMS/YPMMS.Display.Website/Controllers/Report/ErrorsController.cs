using CsvHelper;
using YPMMS.Display.Website.Enums;
using YPMMS.Domain.Service.Services;
using YPMMS.Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YPMMS.Display.Website.Controllers.Report
{
    [RoutePrefix("Report/Errors")]
    public class ErrorsController : BaseController
    {
        private void SetDefaultParamsInViewBag()
        {
            DateTime today = DateTime.Now.Date;
            ViewBag.ReportStart = today.AddDays(-6);
            ViewBag.ReportEnd = today.AddDays(1);
        }

        // GET: Errors
        [Route("")]
        public ActionResult Index()
        {
            SessionHelper.ActiveSection = SiteSection.Reports;

            return View("~/Views/Report/Errors/Index.cshtml");
        }

        //[Route("Statistics", Name = "ErrorsStatistics")]
        //public ActionResult Statistics(DateTime? start, DateTime? end)
        //{
        //    try
        //    {
        //        SessionHelper.ActiveSection = SiteSection.Reports;

        //        var reportService = new ReportErrorDataService(Config.DbConnectionString);

        //        if (start == null || end == null)
        //            SetDefaultParamsInViewBag();
        //        else
        //        {
        //            ViewBag.ReportStart = (DateTime)start;
        //            ViewBag.ReportEnd = ((DateTime)end).Date.AddDays(1);
        //        }

        //        IList<ReportErrorsStatsModel> data = null;

        //        var task = Task.Run(async () =>
        //        {
        //            data = await reportService.GetStatistics(ViewBag.ReportStart, ViewBag.ReportEnd);
        //        });

        //        task.Wait();

        //        return PartialView("~/Views/Report/Errors/_Statistics.cshtml",
        //            data);
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //        return InternalServerError();
        //    }
        //}

        //[Route("MachineDetails", Name = "ErrorsMachineDetails")]
        //public ActionResult MachineDetails()
        //{
        //    try
        //    {
        //        SessionHelper.ActiveSection = SiteSection.Reports;

        //        var machineService = new MerchantDataService(Config.DbConnectionString);

        //        SetDefaultParamsInViewBag();
        //        IList<MachineSite> machines = null;

        //        var task = Task.Run(async () =>
        //        {
        //            machines = await machineService.GetAllMachinesWithSites();
        //        });

        //        task.Wait();

        //        return PartialView("~/Views/Report/Errors/_MachineDetails.cshtml",
        //            machines);
        //    }
        //    catch (Exception e)
        //    {
        //        Log.Error(e);
        //        return InternalServerError();
        //    }
        //}

        //[Route("DownloadCsv", Name = "ErrorsReportDownloadCsv")]
        //public FileResult DownloadCsv(DateTime start, DateTime end)
        //{
        //    var reportService = new ReportErrorDataService(Config.DbConnectionString);

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (StreamWriter tw = new StreamWriter(ms))
        //        {
        //            var csv = new CsvWriter(tw);

        //            IList<dynamic> reportItems = null;
        //            var task = Task.Run(async () =>
        //            {
        //                reportItems = await reportService.GetSummaryCsvData(
        //                    start,
        //                    end
        //                );
        //            });

        //            task.Wait();

        //            csv.WriteRecords(reportItems.ToArray());
        //            tw.Flush();
        //            return File(ms.ToArray(), "text/csv", "ErrorsReport.csv");
        //        }
        //    }
        //}
    }
}