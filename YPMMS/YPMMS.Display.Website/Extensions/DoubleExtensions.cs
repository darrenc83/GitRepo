using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YPMMS.Display.Website.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToHoursMinutes(this double input)
        {
            double hours = Math.Truncate(input);
            double minutes = (input - hours)*60;

            return string.Format("{0:00}:{1:00}", hours, minutes);
        }
    }
}