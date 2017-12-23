using System.ComponentModel;

namespace YPMMS.Shared.Core.Enums
{
    public enum CollectionStatus
    {
        [Description("Started")]
        Started,

        [Description("Collector Refill")]
        CollectorRefill,

        [Description("Manager Refill")]
        ManagerRefill,

        [Description("Complete")]
        Complete,

        #region Error states

        [Description("Started/Interrupted")]
        StartedInterrupted = 0x80,

        [Description("Collector Refill Interrupted")]
        CollectorRefillInterrupted = 0x81,

        [Description("Manager Refill Interrupted")]
        ManagerRefillInterrupted = 0x82,

        [Description("Complete/Interrupted")]
        CompleteInterrupted = 0x83

        #endregion
    }

    public static class CollectionStatusExtensions
    {
        public static bool IsError(this CollectionStatus status)
        {
            return
                status == CollectionStatus.StartedInterrupted ||
                status == CollectionStatus.CollectorRefillInterrupted ||
                status == CollectionStatus.ManagerRefillInterrupted ||
                status == CollectionStatus.CompleteInterrupted;
        }

        public static bool IsActive(this CollectionStatus status)
        {
            return
                status == CollectionStatus.Started||
                status == CollectionStatus.CollectorRefill ||
                status == CollectionStatus.ManagerRefill;
        }
    }
}