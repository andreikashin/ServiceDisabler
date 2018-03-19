using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDisabler.Helpers
{
    internal static class Extensions
    {
        public static DateTime SetTime(this DateTimeOffset currentDateTime, int hours, int minutes, int seconds)
        {
            return new DateTime(
                currentDateTime.Year,
                currentDateTime.Month,
                currentDateTime.Day,
                hours, minutes, seconds);
        }
    }
}
