using System.Collections.Generic;
using System.Linq;
using YPMMS.Shared.Core.Extensions;

namespace YPMMS.Display.Website.Models.Breadcrumb
{
    /// <summary>
    /// A single item within a navigation breadcrumb trail
    /// </summary>
    public sealed class BreadcrumbModel
    {
        /// <summary>
        /// The type of this breadcrumb
        /// </summary>
        public BreadcrumbType Type { get; set; }

        /// <summary>
        /// Any IDs associated with this breadcrumb, in the order they would appear in the URL
        /// </summary>
        public long[] Ids { get; set; }

        /// <summary>
        /// URL for the resource this breadcrumb represents
        /// </summary>
        public string Url
        {
            get
            {
                var parts = new List<string>
                {
                    Type.GetController()
                };
                parts.AddRange(Ids.Select(id => id.ToString()));
                return "/" + string.Join("/", parts);
            }
        }

        /// <summary>
        /// User-readable caption for this breadcrumb
        /// </summary>
        public string Caption => Type.GetDescription();
    }
}