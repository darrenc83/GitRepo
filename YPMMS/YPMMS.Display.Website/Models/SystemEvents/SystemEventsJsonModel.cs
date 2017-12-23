using System.Collections.Generic;
using System.Linq;
using YPMMS.Display.Utilities;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Display.Website.Models.SystemEvents
{
    /// <summary>
    /// Model used to return <see cref="SystemEvent"/> objects to the UI as JSON.
    /// </summary>
    public class SystemEventsJsonModel
    {
        /// <summary>
        /// List of events, sorted in descending date/time order
        /// </summary>
        public IList<SystemEvent> Events { get; set; }

        /// <summary>
        /// Number of events that are unread by the current user
        /// </summary>
        public int UnreadCount => Events?.Count(e => e.IsNew) ?? 0;

        /// <summary>
        /// True if there are (potentially) more events that could be loaded for the current user
        /// </summary>
        public bool MoreAvailable => (Events?.Count ?? 0) == Consts.SystemEventsPageSize;

        public SystemEventsJsonModel(IList<SystemEvent> events)
        {
            Events = events;
        }
    }
}