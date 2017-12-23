using System;
using System.ComponentModel;

namespace YPMMS.Display.Website.Models.Breadcrumb
{
    /// <summary>
    /// Type of a breadcrumb within a breadcrumb trail
    /// </summary>
    public enum BreadcrumbType
    {
        [Description("System Overview")]
        Overview,

        [Description("System Information")]
        Machine,

        [Description("Collections")]
        Collectors,

        [Description("Collector Profile")]
        CollectorProfile,

        [Description("Collection Detail")]
        CollectionDetail,

        [Description("Refill Detail")]
        RefillDetail,

        [Description("Manager Refill")]
        ManagerRefill,

        [Description("System Information")]
        ManagerMachineProfile
    }

    public static class BreadcrumbPartExtensions
    {
        /// <summary>
        /// Get the name of the controller to use in the breadcrumb's URL 
        /// </summary>
        public static string GetController(this BreadcrumbType type)
        {
            switch (type)
            {
                case BreadcrumbType.Overview:
                    return "Overview";
                case BreadcrumbType.Machine:
                    return "Machine";
                case BreadcrumbType.Collectors:
                    return "Collectors";
                case BreadcrumbType.CollectorProfile:
                    return "Collector";
                case BreadcrumbType.CollectionDetail:
                    return "Collection";
                case BreadcrumbType.RefillDetail:
                    return "Collection";
                case BreadcrumbType.ManagerRefill:
                    return "Manager";
                case BreadcrumbType.ManagerMachineProfile:
                    return "Manager";
                default:
                    throw new Exception($"Unrecognised value {type}");
            }
        }
    }
}