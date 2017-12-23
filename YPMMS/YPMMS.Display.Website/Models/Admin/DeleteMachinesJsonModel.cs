using System.Collections.Generic;

namespace YPMMS.Display.Website.Models.Admin
{
    /// <summary>
    /// Model passed in with a Delete Machines request
    /// </summary>
    public sealed class DeleteMachinesJsonModel
    {
        public string Password { get; set; }
        public IList<long> MachineIds { get; set; }
    }
}