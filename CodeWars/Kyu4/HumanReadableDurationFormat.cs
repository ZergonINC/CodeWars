using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars.Kyu4
{
    internal class HumanReadableDurationFormat
    {
        public static string formatDuration(int seconds)
        {
            var time = TimeSpan.FromSeconds(seconds);
            var years = time.Days / 365;
            var days = time.Days % 365;
            if (seconds == 1) return "1 second";
            return seconds == 0 ? "now" : ($"{(years == 0 ? "" : $"{years} {(years > 1 ? "years" : "year")}, ")}" +
                $"{(days == 0 ? "" : $"{days} {(days > 1 ? "days" : "day")}, ")}" +
                $"{(time.Hours == 0 ? "" : $"{time.Hours} {(time.Hours > 1 ? "hours" : "hour")}")}" +
                $"{(time.Minutes == 0 ? "" : $"{(time.Hours == 0 ? "" : time.Seconds == 0 ? " and " : ", ")}{time.Minutes} {(time.Minutes > 1 ? "minutes" : "minute")}")}" +
                $"{(time.Seconds == 0 ? "" : $" and {time.Seconds} {(time.Seconds > 1 ? "seconds" : "second")}")}");
        }
    }
}
