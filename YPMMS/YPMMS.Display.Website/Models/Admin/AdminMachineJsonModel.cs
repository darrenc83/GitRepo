using YPMMS.Shared.Core.Models;

namespace YPMMS.Display.Website.Models.Admin
{
    /// <summary>
    /// A simplified version of <see cref="Machine"/> for use by the Admin UI
    /// </summary>
    public sealed class AdminMachineJsonModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
       // public Site Site { get; set; }
    }
}