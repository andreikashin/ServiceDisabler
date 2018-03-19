using System;

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
                hours, minutes, seconds, 0);
        }
    }
}
