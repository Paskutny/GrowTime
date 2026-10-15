using System;

namespace GrowTime
{
    // Pure helpers without Unity/Valheim dependencies, so they can be unit tested.
    internal static class TimeFormat
    {
        // "12m 34s" below one hour, "1h 05m" from one hour on.
        public static string Format(double seconds)
        {
            // Round up so a growing animal never shows "0m 00s".
            int total = (int)Math.Ceiling(Math.Max(0, seconds));
            int h = total / 3600, m = total / 60 % 60, s = total % 60;
            return h > 0 ? $"{h}h {m:00}m" : $"{m}m {s:00}s";
        }

        public static int Percent(double elapsed, double total)
        {
            return (int)Math.Max(0, Math.Min(100, Math.Floor(elapsed / total * 100)));
        }
    }
}
