using System.Web.Optimization;

namespace YPMMS.Display.Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Style bundles

            bundles.Add(new StyleBundle("~/Content/css/default").Include(
                "~/Content/css/normalize.css",
                "~/Content/css/bootstrap.css",
                "~/Content/css/form-styles.css",
                "~/Content/css/sprites.css",
                "~/Content/css/animations.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/daterangepicker.css",
                "~/Content/css/style.css"
                ));

            #endregion

            #region Script bundles

            bundles.Add(new ScriptBundle("~/bundles/js/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/js/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.kinetic.js",
                "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/selectableScroll").Include(
                "~/Scripts/jquery-ui.js",
                "~/Scripts/selectableScroll.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/markerclusterer").Include(
                "~/Scripts/markerclusterer.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/ViewsAdminAddSystem").Include("~/Scripts/Views/Admin.AddSystem.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsAdminIndex").Include("~/Scripts/Views/Admin.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsAdminMerchantManagement").Include("~/Scripts/Views/Admin.MerchantManagement.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsCollectorIndex").Include("~/Scripts/Views/Collector.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsCollectorsIndex").Include("~/Scripts/Views/Collectors.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsMachineIndex").Include("~/Scripts/Views/Machine.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsHistoryIndex").Include("~/Scripts/Views/History.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsMachineEvents").Include("~/Scripts/Views/Machine.Events.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsManagerIndex").Include("~/Scripts/Views/Manager.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsManagerRefillSystem").Include("~/Scripts/Views/Manager.RefillSystem.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsOverviewIndex").Include("~/Scripts/Views/Overview.Index.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsReportOffline").Include("~/Scripts/Views/Report.Offline.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsReportHopper").Include("~/Scripts/Views/Report.Hopper.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsReportErrors").Include("~/Scripts/Views/Report.Errors.js"));
            bundles.Add(new ScriptBundle("~/bundles/js/ViewsReportRoot").Include("~/Scripts/Views/Report.Root.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/default").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.tablesorter.js",
                "~/Scripts/respond.js",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jquery.kinetic.js",
                "~/Scripts/selectableScroll.js",
                "~/Scripts/jquery.capslockstate.js",
                "~/Content/js/touch-gestures.js",
                "~/Scripts/Chart.min.js",
                "~/Content/js/plugins.js",
                "~/Content/js/main.js",
                "~/Content/js/tablesorter-custom.js",
                "~/Scripts/smoothscroll.min.js",
                "~/Scripts/moment.js",
                "~/Scripts/daterangepicker.js",
                "~/Scripts/jquery.signalR-2.2.0.min.js",
                "~/Scripts/google-analytics.js",
                "~/Scripts/Views/Shared/DatePicker.js",
                "~/Scripts/Views/Shared/Format.js",
                "~/Scripts/Views/Shared/MapDefaults.js",
                "~/Scripts/Views/Shared/Tools.js",
                "~/Scripts/Views/Shared/SystemEvents.js",
                "~/Scripts/Views/Shared/Header.js"));

            #endregion

#if !DEBUG
            // Force minification in release builds
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
