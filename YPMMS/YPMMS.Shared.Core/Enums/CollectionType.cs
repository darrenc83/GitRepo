using System.ComponentModel;

namespace YPMMS.Shared.Core.Enums
{
    public enum CollectionType
    {
        [Description("Collection")]
        Collection,

        [Description("Collector Refill")]
        CollectorRefill,

        [Description("Manager Refill")]
        ManagerRefill,

        #region Error states

        [Description("Collection Interrupted")]
        CollectionInterrupted = 0x80,

        [Description("Collector Refill Interrupted")]
        CollectorRefillInterrupted = 0x81,

        [Description("Manager Refill Interrupted")]
        ManagerRefillInterrupted = 0x82

        #endregion
    }

    public static class CollectionTypeExtensions
    {
        public static bool IsCollection(this CollectionType type)
        {
            return
                type == CollectionType.Collection ||
                type == CollectionType.CollectionInterrupted;
        }

        public static bool IsManagerRefill(this CollectionType type)
        {
            return
                type == CollectionType.ManagerRefill ||
                type == CollectionType.ManagerRefillInterrupted;
        }

        public static bool IsError(this CollectionType type)
        {
            return
                type == CollectionType.CollectionInterrupted ||
                type == CollectionType.CollectorRefillInterrupted ||
                type == CollectionType.ManagerRefillInterrupted;
        }
    }
}