using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPMMS.Shared.Core.Models
{
    public sealed class StatusEvent
    {
        public long Id { get; set; }
        public long MachineId { get; set; }
        public string EventType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
