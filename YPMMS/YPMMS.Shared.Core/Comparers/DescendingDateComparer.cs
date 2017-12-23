using System;
using System.Collections.Generic;

namespace YPMMS.Shared.Core.Comparers
{
    public sealed class DescendingDateComparer : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return y.CompareTo(x);
        }
    }
}
