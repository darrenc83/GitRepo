using System;
using System.Collections.Generic;
using YPMMS.Shared.Core.Models;

namespace YPMMS.Shared.Core.Comparers
{
    /// <summary>
    /// Simple comparer to order machines in ascending order by collection events into descending date order
    /// </summary>
    public sealed class AscendingMerchantComparer : IComparer<Merchant>
    {
        public int Compare(Merchant x, Merchant y)
        {
            return string.Compare(x.MerchantName, y.MerchantName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
