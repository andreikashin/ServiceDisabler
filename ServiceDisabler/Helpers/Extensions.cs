using System;

namespace ServiceDisabler.Helpers
{
    internal static class Extensions
    {
        /// <summary>
        /// Update hours, minutes and seconds in particular DateTime
        /// </summary>
        /// <param name="currentDateTime">The Current DateTime</param>
        /// <param name="hours">Hours</param>
        /// <param name="minutes">Minutes</param>
        /// <param name="seconds">Seconds</param>
        /// <returns></returns>
        public static DateTime SetTime(this DateTime currentDateTime, int hours, int minutes, int seconds)
        {
            var newDate = new DateTime(
                currentDateTime.Year,
                currentDateTime.Month,
                currentDateTime.Day,
                hours, minutes, seconds, 0);
            return newDate;
        }
    }
}
