using System;

namespace JScrambler.Client
{
    internal static class NullableDateTimeExtensions
    {
        private static readonly string ISO8601Mask = "yyyy-MM-ddTHH:mm:sszzz";

        public static string ToISO8601String(this DateTime? date)
        {
            return date.HasValue
                ? date.Value.ToString(ISO8601Mask)
                : DateTime.Now.ToString(ISO8601Mask);
        }
    }
}