using YPMMS.Shared.Core.Models;

namespace YPMMS.Data.Repository.Models
{
    /// <summary>
    /// Utility class used for adding a <see cref="SystemEvent"/> to the database,
    /// as we need to convert the enum propeties to strings before passing to Dapper.
    /// </summary>
    public class SystemEventInsertModel
    {
        public string EventType { get; set; }
        public string MachineStatus { get; set; }
        public long? MachineId { get; set; }
        public string MachineName { get; set; }
        public long? SiteId { get; set; }
        public string SiteName { get; set; }
        public long? CollectorId { get; set; }
        public string UserId { get; set; }
        public long? CollectionEventId { get; set; }
    }
}