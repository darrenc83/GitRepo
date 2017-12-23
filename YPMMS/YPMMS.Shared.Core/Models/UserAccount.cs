using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPMMS.Shared.Core.Models
{
    /// <summary>
    /// Model to represent a UserAccount
    /// </summary>
    public sealed class UserAccount
    {
        public long Id { get; set; }
        public string Organisation { get; set; }
        public string Department { get; set; }
        public string Logo { get; set; }
        public DateTime Created;
    }
}
