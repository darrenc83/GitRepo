using YPMMS.Shared.Core.Models;

namespace YPMMS.Data.Repository.Models
{
    /// <summary>
    /// Utility class used for adding a <see cref="Machine"/> to the database,
    /// as we need to convert the enum propeties to strings before passing to Dapper.
    /// </summary>
    public class MerchantInsertModel
    {
       public long Id { get; set; }
        public string Name { get; set; }
        public long SiteId { get; set; }
        public string SystemType { get; set; }
        public string Status { get; set; }
        public long Account { get; set; }

    }
}