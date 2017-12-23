using System.Collections.Generic;

namespace YPMMS.Display.Website.Models.Breadcrumb
{
    /// <summary>
    /// A collection of one or more breadcrumb(s) to display in the navigation strip at the top of a page
    /// </summary>
    public sealed class BreadcrumbTrailModel
    {
        /// <summary>
        /// The breadcrumbs comprising this trail, in the order in which they should appear
        /// </summary>
        public List<BreadcrumbModel> Breadcrumbs { get; } = new List<BreadcrumbModel>();

        /// <summary>
        /// Add a breadcrumnb to this trail
        /// </summary>
        /// <param name="type">Type of breadcrumb to add</param>
        /// <param name="ids">IDs (if any) to use in this breadcrumb's URL</param>
        /// <returns>Reference to this, so calls can be chained in a fluent style</returns>
        public BreadcrumbTrailModel Add(BreadcrumbType type, params long[] ids)
        {
            Breadcrumbs.Add(new BreadcrumbModel
            {
                Type = type,
                Ids = ids
            });

            return this;
        }
    }
}